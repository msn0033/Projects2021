using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces.CollectCashCheck;
using ERPv1.ERP.CurrentAssetModules.Checks.Model;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.CollectCashCheck;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Services.CollectCashCheck
{
    public class CollectCashCheckManager : ICollectCashCheckManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IJournalManager _journalManager;
        private readonly INRManager _nRManager;

        public CollectCashCheckManager(ApplicationDbContext db, IJournalManager journalManager ,INRManager nRManager)
        {
            _db = db;
            _journalManager = journalManager;
            _nRManager = nRManager;
        }
        public CollectCashCheckContainerVM NewCollectCashCheck(string ChkNum)
        {
            var vm = new CollectCashCheckContainerVM();
            vm.ClientData = _db.Check.Include(x => x.Contact).Where(x => x.ChkNum == ChkNum).Select(x => new ClientData()
            {
                ClientId = x.Contact.Id,
                ClientName = x.Contact.NameAr,
                Balance = x.Contact.ClientBalance,
                Phone = x.Contact.Phone1,
                TaxationCard = x.Contact.TaxationCard
            }).FirstOrDefault();

            vm.SelectedCheck = _db.Check
                .Include(x => x.CheckStatus)
                .Include(x => x.Currency)
                .Include(x=>x.BankAccDetails)
                .Where(x => x.ChkNum == ChkNum).Select(x => new SelectedCheck()
                {
                    ChKNum = x.ChkNum,
                    CheckStatus = x.CheckStatus.CheckStatusAR,
                    ContactId = x.ContactId,
                    CurrencyAbbr = x.Currency.CurrencyAbbrev,
                    DueDate = x.DueDate.ToString(),
                    OrignalBankName = x.OrginalBank,
                    AmountForgin = x.AmountForgin,
                    AmountLocal = x.AmountLocal,
                    Paid=x.Paid,
                    Currency=x.Currency
                }).FirstOrDefault();

            return vm;
        }

        public void SaveCollectCashCheck(CollectCashCheckContainerVM vm)
        {
            //Journal
            string JournalId = SaveCheckJournal(vm);
            //update Check
            UpdateCheck(vm);
            //if Check
            IfCheck(vm, JournalId);
        }
        private string SaveCheckJournal(CollectCashCheckContainerVM vm)
        {
            //Journal
            var J = new JournalVM();
            J.TransDate = vm.PaymentDetails.PaymentDate;
            J.TransDes = vm.PaymentDetails.Description;
            //Debit
            var J_Debit = new JournalDetailsVM();
            J_Debit.AccNum = vm.PaymentDetails.PaymentMethod == ClientPaymentMethodEnum.Safe ? vm.PaymentDetails.SafeAccNum
                             : vm.PaymentDetails.PaymentMethod == ClientPaymentMethodEnum.Bank ? vm.PaymentDetails.BankAccNum
                             : "1240000001";//1240000001 شيكات في الخزنة
            J_Debit.Side = JournalSideEnum.Debit;
            J_Debit.Debit = vm.PaymentDetails.PaymentAmount;
            J_Debit.CurrencyId = vm.SelectedCheck.Currency.Id;
            J_Debit.UsedRate = vm.SelectedCheck.Currency.Rate;
            J_Debit.CurrencyAbbr = vm.SelectedCheck.Currency.CurrencyAbbrev;
            J.JournalDetailsVM.Add(J_Debit);
            //
            //Credit
            var J_Credit = new JournalDetailsVM();
            J_Credit.AccNum = "1240000001";// 1240000001 شيكات في الخزنة
            J_Credit.Side = JournalSideEnum.Credit;
            J_Credit.Credit = vm.PaymentDetails.PaymentAmount;
            J_Credit.CurrencyId = vm.SelectedCheck.Currency.Id;
            J_Credit.UsedRate = vm.SelectedCheck.Currency.Rate;
            J_Credit.CurrencyAbbr = vm.SelectedCheck.Currency.CurrencyAbbrev;
            J.JournalDetailsVM.Add(J_Credit);

            //Credit Client
            if (J_Debit.AccNum != "1240000001")//لاينفذ في حال الشيك كان استبدال رجيع
            {
                var x = _db.Contacts.Where(x => x.Id == vm.ClientData.ClientId).Select(x => x.ClientAccNum);
                var J_CreditClient = new JournalDetailsVM();
                J_CreditClient.AccNum =x.FirstOrDefault();// حساب العميل
                J_CreditClient.Side = JournalSideEnum.Credit;
                J_CreditClient.Credit = vm.PaymentDetails.PaymentAmount;
                J_CreditClient.CurrencyId = vm.SelectedCheck.Currency.Id;
                J_CreditClient.UsedRate = vm.SelectedCheck.Currency.Rate;
                J_CreditClient.CurrencyAbbr = vm.SelectedCheck.Currency.CurrencyAbbrev;
                J.JournalDetailsVM.Add(J_CreditClient);

            }
            if (J_Debit.AccNum == "1240000001")//ينفذ في حال الشيك كان استبدال رجيع
            {
                var D_Debit = new JournalDetailsVM();
                D_Debit.AccNum = "1240000003";//التسميع في الشيكات الرجيعة
                D_Debit.Side = JournalSideEnum.Debit;
                D_Debit.Debit = vm.SelectedCheck.AmountLocal;
                D_Debit.CurrencyId = vm.SelectedCheck.Currency.Id;
                D_Debit.UsedRate = vm.SelectedCheck.Currency.Rate;
                D_Debit.CurrencyAbbr = vm.SelectedCheck.Currency.CurrencyAbbrev;
                J.JournalDetailsVM.Add(D_Debit);
            }
            string JournalId = _journalManager.SaveJournal(J);
            return JournalId;
        }
        private void UpdateCheck(CollectCashCheckContainerVM vm)
        {
            if (vm.PaymentDetails.PaymentMethod != ClientPaymentMethodEnum.Check)
            {
                var Ch = _db.Check.FirstOrDefault(x => x.ChkNum == vm.SelectedCheck.ChKNum);
                Ch.Paid = Ch.Paid + vm.PaymentDetails.PaymentAmount;
                Ch.UnPaid = Ch.AmountLocal - Ch.Paid;
                if (Ch.Paid == Ch.AmountLocal)
                {
                    Ch.CheckStatusId = 2;//محصل
                    Ch.CheckLocationId = 5;// تم استلام مبلغ الشيك بالكامل نقداً
                }
                _db.Check.Update(Ch);
                _db.SaveChanges();
            
            }
        }
        private void IfCheck(CollectCashCheckContainerVM vm, string JournalId)
        {
            if (vm.PaymentDetails.PaymentMethod == ClientPaymentMethodEnum.Check)
            {
                //UpdateCheck Old Check
                var Ch = _db.Check.FirstOrDefault(x => x.ChkNum == vm.SelectedCheck.ChKNum);
                Ch.CheckStatusId = 3;//رجيع
                Ch.CheckLocationId = 4;// مع العميل
                _db.Check.Update(Ch);
                _db.SaveChanges();

                //add new Check
                var newCheck = new ClientPaymentContainer();
                newCheck.PaymentDetails.CheckNum = vm.PaymentDetails.CheckNum;
                newCheck.PaymentDetails.PaymentAmount = vm.PaymentDetails.PaymentAmount * vm.SelectedCheck.Currency.Rate;
                newCheck.ClientData.ClientId = vm.ClientData.ClientId;
                newCheck.PaymentDetails.OriginalBank = vm.PaymentDetails.OriginalBank;
                newCheck.PaymentDetails.PaymentDueDate = vm.PaymentDetails.PaymentDueDate;
                newCheck.PaymentDetails.PaymentDate = vm.PaymentDetails.PaymentDate;
               _nRManager.AddNewCheck(newCheck, vm.SelectedCheck.Currency, JournalId);
            }
        }


      
    }
}

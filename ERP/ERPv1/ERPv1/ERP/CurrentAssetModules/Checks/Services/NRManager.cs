using ERPv1.Data;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using ERPv1.ERP.CurrentAssetModules.Checks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.Infrastructure.Extensions;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInSafe;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInBank;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Services
{
    public class NRManager : INRManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IJournalManager _journalManager;

        public NRManager(ApplicationDbContext db ,IJournalManager journalManager)
        {
            _db = db;
            _journalManager = journalManager;
        }

        public void AddNewCheck(ClientPaymentContainer vm, Currency Currency, string JournalId)
        {  
            var Check = new Check();
            Check.ChkNum = vm.PaymentDetails.CheckNum;
            Check.DueDate = vm.PaymentDetails.PaymentDueDate.ConvertDate();
            Check.CurrencyId = Currency.Id;
            Check.AmountLocal = vm.PaymentDetails.PaymentAmount * Currency.Rate;
            Check.AmountForgin = vm.PaymentDetails.PaymentAmount;
            Check.ContactId = vm.ClientData.ClientId;
            Check.OrginalBank = vm.PaymentDetails.OriginalBank;
            Check.Paid = 0;
            Check.UnPaid = vm.PaymentDetails.PaymentAmount;
            Check.CheckStatusId = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
            Check.CheckLocationId = _db.CheckLocation.FirstOrDefault(x => x.IsDefault).Id;
            
            _db.Check.Add(Check);
            _db.SaveChanges();

            _db.CheckHistory.Add(new CheckHistory()
            {
                ChkNum = Check.ChkNum,
                Description = $" {vm.PaymentDetails.CheckNum}شيك جديد",
                TransDate = vm.PaymentDetails.PaymentDate.ConvertDate(),
                TransID = JournalId,
            });

            _db.SaveChanges();


        }
        public CheckInSafeContainerVM GetCheckInSafe()
        {
            var vm = new CheckInSafeContainerVM();
            var CheckStatus = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
            //شيكات في الخزنة
            {
                vm.CheckInSafeDetailsVM = _db.Check.Include(x => x.Contact)
                    .Include(x => x.CheckStatus)
                    .Include(x => x.Currency)
                    .Where(x => (x.CheckStatusId == CheckStatus  && x.CheckLocationId == 1))
                    .Select(x => new CheckInSafeDetailsVM()
                    {
                        CheckAmount = x.AmountLocal,
                        CheckAmountForiegn = x.AmountForgin,
                        CurrencyAbbr = x.Currency.CurrencyAbbrev,
                        CheckNum = x.ChkNum,
                        ClientName = x.Contact.NameAr,
                        CheckStatus = x.CheckStatus.CheckStatusAR,
                        DueDate = x.DueDate.Value.ToShortDateString(),
                        OrginalBank = x.OrginalBank,
                        Paid = x.Paid,
                        UnPaid = x.UnPaid,
                        Selected = false
                    }
                    ).ToList();
            } //شيكات في الخزنة

            return vm;

        }//end Method
        public void MoveToBank (CheckInSafeContainerVM vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    //Create New Hafza
                    CheckHafza Hafza = AddNewHafza(vm);

                    //Selected check selected= true
                    var SelectedChecks = vm.CheckInSafeDetailsVM.Where(x => x.Selected == true);

                    //Total selected All selected
                    var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);

                    //Journal Transaction القيد المحاسبي
                    string JournalId = MoveToBankJournal(vm, CheckTotalAmount);

                    //Update Check Table
                    UpdateMovedToBankChecks(vm, Hafza, SelectedChecks, JournalId);
                    
                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
            }

        }

        private void UpdateMovedToBankChecks(CheckInSafeContainerVM vm, CheckHafza Hafza, IEnumerable<CheckInSafeDetailsVM> SelectedChecks, string JournalId)
        {
            foreach (var item in SelectedChecks)
            {
                var Chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
                Chk.CheckLocationId = 2;// 2 البنك
                Chk.CheckHafzaId = Hafza.Id;
                Chk.BankAccNum = vm.HafzaDetailsVM.BankAccNum;
                //_db.Update(Chk);
                //_db.SaveChanges();
                _db.CheckHistory.Add(new CheckHistory()
                {
                    ChkNum = item.CheckNum,
                    TransDate = Hafza.HafzaDate,
                    TransID = JournalId,
                    Description = "نقل الشيك الى البنك"
                });
            }
        }

        private string MoveToBankJournal(CheckInSafeContainerVM vm, decimal CheckTotalAmount)
        {
            var J = new JournalVM();
            J.TransDate = DateTimeOffset.Now.ToString("dd/MM/yyyy");
            J.TransDes = $"نقل الشيك الى البنك من خلال الحافظة{vm.HafzaDetailsVM.HafzaName} :";

            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = "1240000002"; // 1240000002 حساب شيكات في البنك
            JD_Debit.Side = JournalSideEnum.Debit;
            JD_Debit.Debit = CheckTotalAmount;
            JD_Debit.CurrencyId = 1;
            JD_Debit.UsedRate = 1; 
            J.JournalDetailsVM.Add(JD_Debit);

            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = "1240000001";//1240000001حساب شيكات في الخزنة 
            JD_Credit.Credit = CheckTotalAmount;
            JD_Credit.Side = JournalSideEnum.Credit;
            JD_Credit.CurrencyId = 1;
            JD_Credit.UsedRate = 1;
           
            J.JournalDetailsVM.Add(JD_Credit);
            string JournalId = _journalManager.SaveJournal(J);
            return JournalId;
        }

        private CheckHafza AddNewHafza(CheckInSafeContainerVM vm)
        {
            var Hafaza = new CheckHafza();
            Hafaza.BankAccNum = vm.HafzaDetailsVM.BankAccNum;
            Hafaza.HafzaName = vm.HafzaDetailsVM.HafzaName;
            Hafaza.HafzaDate = vm.HafzaDetailsVM.HafzaDate.ConvertDate();
            _db.CheckHafza.Add(Hafaza);
            _db.SaveChanges();
            return Hafaza;
        }

        public CheckInBankContainerVM GetCheckInBank()
        {
            var vm = new CheckInBankContainerVM();
            vm.CheckInBankDetailsVM = _db.Check.Include(x => x.Contact)
                                   .Include(x => x.CheckStatus)
                                   .Where(x => x.CheckStatusId == 1 && x.CheckLocationId == 2)
                                   .Select(x => new CheckInBankDetailsVM()
                                   {
                                       CheckAmount=x.AmountLocal,
                                       CheckNum=x.ChkNum,
                                       ClientName=x.Contact.NameAr,
                                       CheckStatus=x.CheckStatus.CheckStatusAR,
                                       DueDate=x.DueDate.Value.ToString("dd/MM/yyyy"),
                                       OrginalBank=x.OrginalBank,
                                       Selected=false,
                                       BankAccName=x.BankAccDetails.AccountNameAr,
                                       BankAccNum=x.BankAccNum  
                                   }).ToList();
            return vm;
        }

        public void CollectChecks(CheckInBankContainerVM vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                   

                    //Selected check selected= true
                    var SelectedChecks = vm.CheckInBankDetailsVM.Where(x => x.Selected == true);

                    ////Total selected All selected
                    //var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);

                    //Journal Transaction القيد المحاسبي
                    string JournalId = CollectionJournal(SelectedChecks);

                    //Update Check Table
                    UpdateCollectedCheckInfo(SelectedChecks, JournalId);

                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
            }

        }
        private string CollectionJournal(IEnumerable<CheckInBankDetailsVM> SelectedChecks)
        {
            var J = new JournalVM();
            
            //sum and groupBy
            var SelectedByBank = SelectedChecks.GroupBy(x =>new { x.BankAccNum } )
                                 .Select(x => new
                                 {
                                     BankAccountNum=x.Key.BankAccNum,
                                     TotalAmount=x.Sum(y=>y.CheckAmount)
                                 });

            J.TransDate = DateTimeOffset.Now.ToString("dd/MM/yyyy");
            J.TransDes = $" Check Collectionتحصيل شيكات";
            foreach (var item in SelectedByBank)
            {
                var JD_Debit = new JournalDetailsVM();
                JD_Debit.AccNum = item.BankAccountNum;
                JD_Debit.Side = JournalSideEnum.Debit;
                JD_Debit.Debit = item.TotalAmount;
                JD_Debit.CurrencyId = 1;
                JD_Debit.UsedRate = 1;
                J.JournalDetailsVM.Add(JD_Debit);

            }

            //Total All in SelectedChecks for Use Credit
            var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);

            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = "1240000002"; // 1240000002 حساب شيكات في البنك
            JD_Credit.Credit = CheckTotalAmount;
            JD_Credit.Side = JournalSideEnum.Credit;
            JD_Credit.CurrencyId = 1;
            JD_Credit.UsedRate = 1;

            J.JournalDetailsVM.Add(JD_Credit);
            string JournalId = _journalManager.SaveJournal(J);
            return JournalId;
        }
        private void UpdateCollectedCheckInfo(IEnumerable<CheckInBankDetailsVM> SelectedChecks, string JournalId)
        {
            foreach (var item in SelectedChecks)
            {
                var Chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
                Chk.CheckStatusId = 2;
                Chk.CheckLocationId = 3;

                //_db.Update(Chk);
                //_db.SaveChanges();
                _db.CheckHistory.Add(new CheckHistory()
                {
                    ChkNum = item.CheckNum,
                    TransDate = DateTimeOffset.Now.DateTime,
                    TransID = JournalId,
                    Description = $"شيك رقم  {item.CheckNum}  تم تحصيلة بنجاح"
                });
            }
        }

    }
}

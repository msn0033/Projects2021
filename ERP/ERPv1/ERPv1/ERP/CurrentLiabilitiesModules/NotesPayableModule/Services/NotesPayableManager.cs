using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Services
{
    public class NotesPayableManager : INotesPayableManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IJournalManager _journalManager;

        public NotesPayableManager(ApplicationDbContext db, IMapper mapper, IJournalManager journalManager)
        {
            _db = db;
            _mapper = mapper;
            _journalManager = journalManager;
        }
        public void SaveNewNP(NotesPayableCreationVM vm)
        {
            var notes = _mapper.Map<NotesPayable>(vm);
            notes.CheckStatus = NotesPayableStatusEnum.UnderCollection;
            _db.NotesPayables.Add(notes);
            _db.SaveChanges();
        }
        public void SaveNewNPHistory(NotesPayableCreationVM vm, string TransId)
        {
            var His = new NotesPayableTransactionHistory()
            {
                ActionDate = vm.WritingDate.ConvertDate(),
                ChkNum = vm.ChkNum,
                Description = $"أضافة شيك جديد رقم {vm.ChkNum}",
                PaidAmount = 0,
                StatusAfterAction = NotesPayableStatusEnum.UnderCollection,
                TransId = TransId
            };
            _db.NotesPayableTransactionHistory.Add(His);
            _db.SaveChanges();
        }
        public NPContainer GetAllNP()
        {
            var vm = new NPContainer();
            vm.CheckUnderCollection = GetNPWithStatus(NotesPayableStatusEnum.UnderCollection);
            vm.CheckCashCollection =  GetNPWithStatus(NotesPayableStatusEnum.CashCollection);
            
            return vm;
        }
        public List<NPDetails> GetNPWithStatus(NotesPayableStatusEnum statusEnum) 
        {
           
            var NPs = new List<NPDetails>();
            var vm = _db.NotesPayables.Include(x => x.Currency)
                                       .Include(x => x.Supplier)
                                       .Include(x=>x.BankAccount)
                                       .Where(x => x.CheckStatus == statusEnum).ToList();
                
            var map = _mapper.Map<List<NPDetails>>(vm);

            return map;
        }
        public void MoveCheckToCashPayment(NPDetails np)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    check.CheckStatus = NotesPayableStatusEnum.CashCollection;
                    _db.NotesPayables.Update(check);
                    _db.SaveChanges();

                    var His = new NotesPayableTransactionHistory()
                    {
                        ActionDate = DateTimeOffset.UtcNow.ToString().ConvertDate(),
                        ChkNum = np.ChkNum,
                        Description = $"تحصيل نقدي لشيك رقم  {np.ChkNum}",
                        PaidAmount = 0,
                        StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                        TransId = string.Empty
                    };

                    _db.NotesPayableTransactionHistory.Add(His);
                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    transaction.Rollback();
                }
            }
        }

        public void CollectNP(NPDetails np)
        {
            //update Status=>Collected
            //History =>Collected
            //Journal transaction Debit NP -  Credit BankAccount

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    check.CheckStatus = NotesPayableStatusEnum.Collected;
                    _db.NotesPayables.Update(check);
                    _db.SaveChanges();

                    var currency = _db.Currency.Find(np.CurrencyId);
                    var journal = new JournalVM();
                    journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
                    journal.TransDes = $"تحصيل  لشيك رقم  {np.ChkNum} من حساب بنك {np.BankName}";
                    var JD_Debit = new JournalDetailsVM();
                    JD_Debit.AccNum = "2210000001";
                    JD_Debit.Side = JournalSideEnum.Debit;
                    JD_Debit.Debit = np.AmountLocal;
                    JD_Debit.CurrencyId = np.CurrencyId;
                    JD_Debit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_Debit);

                    var JD_Credit = new JournalDetailsVM();
                    JD_Credit.AccNum = np.BankAccountNum;
                    JD_Credit.Side = JournalSideEnum.Credit;
                    JD_Credit.Credit = np.AmountLocal;
                    JD_Credit.CurrencyId =np.CurrencyId;
                    JD_Credit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_Credit);

                    var TransId = _journalManager.SaveJournal(journal);

                    var His = new NotesPayableTransactionHistory()
                    {
                        ActionDate = DateTimeOffset.UtcNow.ToString().ConvertDate(),
                        ChkNum = np.ChkNum,
                        Description = $"تحصيل  لشيك رقم  {np.ChkNum}",
                        PaidAmount = 0,
                        StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                        TransId = TransId
                    };
                    _db.NotesPayableTransactionHistory.Add(His);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    transaction.Rollback();
                }
            }

        }//تحصيل شيك

        public void CollectCashNP(NPDetails np,PaymentDetails paymentDetails)
        {
            //update Status=>Collected
            //History =>Collected
            //Journal transaction Debit NP -  Credit BankAccount

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
                    
                    check.Paid = (check.Paid.HasValue?check.Paid.Value:0) + paymentDetails.PaymentAmount;//HasValue لو لها قيمة جب القيمة او اعمل لها صفر 
                    if (check.Paid.Value==check.AmountForgin)
                       check.CheckStatus = NotesPayableStatusEnum.Collected;
                    else
                        check.CheckStatus = NotesPayableStatusEnum.CashCollection;
                   
                    var journal = new JournalVM();
                    var currency = _db.Currency.Find(np.CurrencyId);
                    journal.TransDate = DateTimeOffset.UtcNow.ToString("dd/MM/yyyy");
                    journal.TransDes = $"تحصيل  لشيك رقم  {np.ChkNum} من حساب بنك {np.BankName}";
                    var JD_Debit = new JournalDetailsVM();
                    JD_Debit.AccNum = "2210000001";
                    JD_Debit.Side = JournalSideEnum.Debit;
                    JD_Debit.Debit = paymentDetails.PaymentAmount;
                    JD_Debit.CurrencyId = np.CurrencyId;
                    JD_Debit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_Debit);

                    var JD_Credit = new JournalDetailsVM();
                    JD_Credit.AccNum = paymentDetails.PaymentMethod == SupplierPaymentMethodEnum.Safe ? paymentDetails.SafeAccNum
                                      : paymentDetails.PaymentMethod == SupplierPaymentMethodEnum.Bank ? paymentDetails.BankAccNum
                                      : "2210000001";
                    JD_Credit.Side = JournalSideEnum.Credit;
                    JD_Credit.Credit = paymentDetails.PaymentAmount;
                    JD_Credit.CurrencyId = np.CurrencyId;
                    JD_Credit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_Credit);

                    var TransId = _journalManager.SaveJournal(journal);
                    //اذا الشيك تم سدادة بالكامل 
                    if(check.CheckStatus==NotesPayableStatusEnum.Collected)
                    {
                        var His = new NotesPayableTransactionHistory()
                        {
                            ActionDate = DateTimeOffset.UtcNow.ToString().ConvertDate(),
                            ChkNum = np.ChkNum,
                            Description = $"تحصيل  لشيك رقم  {np.ChkNum}",
                            PaidAmount = 0,
                            StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                            TransId = TransId
                        };
                        _db.NotesPayableTransactionHistory.Add(His);
                    }
                    //اذا الشيك لم يسدد بالكامل  تسديد دفعات 
                    else
                    {
                        var His = new NotesPayableTransactionHistory()
                        {
                            ActionDate = DateTimeOffset.UtcNow.ToString().ConvertDate(),
                            ChkNum = np.ChkNum,
                            Description = $"تحصيل نقدي لشيك رقم  {np.ChkNum} بمبلغ {paymentDetails.PaymentAmount}",
                            PaidAmount = paymentDetails.PaymentAmount,
                            StatusAfterAction = NotesPayableStatusEnum.CashCollection,
                            TransId = TransId
                        };
                        _db.NotesPayableTransactionHistory.Add(His);
                    }

                    // اذا الحاله شيك =>اضيف شيك جديد
                    if(paymentDetails.PaymentMethod==SupplierPaymentMethodEnum.check)
                    {
                        var newCheck = new NotesPayableCreationVM()
                        {

                            AmountForgin = paymentDetails.PaymentAmount,
                            AmountLocal = paymentDetails.PaymentAmount * currency.Rate,
                            BankAccountNum = paymentDetails.BankAccNum,
                            ChkNum = paymentDetails.CheckNum,
                            CurrencyId = np.CurrencyId,
                            DueDate = paymentDetails.PaymentDueDate,
                            WritingDate = paymentDetails.WritingDate,
                            SupplierId = np.SupplierId,
                        };
                        SaveNewNP(newCheck);
                        SaveNewNPHistory(newCheck,TransId);

                    }
                   
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    transaction.Rollback();
                }
            }

        }//تحصيل شيك و سداد عن طريق 

    }
}

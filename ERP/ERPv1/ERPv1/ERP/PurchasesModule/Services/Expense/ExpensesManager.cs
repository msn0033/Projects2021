using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Interfaces.Expense;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.Services;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services.Expense
{
    public class ExpensesManager : IExpensesManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountGenerator _accountGenerator;
        private readonly IMapper _mapper;
        private readonly ISupplierJournalsManager _supplierJournalsManager;
        private readonly ISupplierBalanceManager _supplierBalanceManager;
        private readonly ISupplierTransactionManager _supplierTransactionManager;
        private readonly INotesPayableManager _notesPayableManager;

        public ExpensesManager(ApplicationDbContext db, IAccountGenerator accountGenerator,IMapper mapper
                            , ISupplierJournalsManager supplierJournalsManager
                            , ISupplierBalanceManager supplierBalanceManager
                            , ISupplierTransactionManager supplierTransactionManager
                            , INotesPayableManager notesPayableManager)
        {
            _db = db;
            _accountGenerator = accountGenerator;
            _mapper = mapper;
            _supplierJournalsManager = supplierJournalsManager;
            _supplierBalanceManager = supplierBalanceManager;
            _supplierTransactionManager = supplierTransactionManager;
            _notesPayableManager = notesPayableManager;

        }
        public List<ExpenseItem> GetAllExpenseItem() =>
            _db.ExpenseItems.Include(x => x.ExpenseType).ToList();

        public void AddNewExpenseItem(ExpenseCreationVM vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var expense = new ExpenseItem();
                    expense.ExpenseName = vm.ExpenseName;
                    expense.ExpenseTypeId = vm.ExpenseTypeId;


                    var account = new CreateAccountVM();
                    account.AccountName = vm.ExpenseName;
                    account.AccountNameAr = vm.ExpenseName;
                    account.AccTypeId = 20;
                    account.BranchId = 1;
                    account.CurrencyId = 1;
                    expense.AccNum = _accountGenerator.CreateNewAccount(account);
                    _db.ExpenseItems.Add(expense);
                    _db.SaveChanges();

                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    var mes = ex.Message;
                    transaction.Rollback();
                }
            }

        }
        public void SaveNewExpense(ExpenseContainerVM vm)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    var currency = _db.Currency.Find(vm.ExpenseDetails.CurrencyId);
                    var ExpenseItem = _db.ExpenseItems.Find(vm.ExpenseDetails.ExpenseItemId);//فئة المصروف - كهرباء - جوالات-بنزين- 

                   

                    var Supplier= vm.ExpenseDetails.SupplierId != null ? _db.Contacts.FirstOrDefault(x => x.Id == vm.ExpenseDetails.SupplierId):null;
                    //1- save in ExpenseSummary
                    var ExpenSummary = _mapper.Map<ExpenseSummary>(vm.ExpenseDetails);
                    ExpenSummary.LocalAmount = vm.ExpenseDetails.Amount * currency.Rate;
                    _db.ExpenseSummaries.Add(ExpenSummary);
                    _db.SaveChanges();


                    
                    //2-Journal Transaction
                    var TransId = _supplierJournalsManager.ExpenseJournal(vm, ExpenseItem.AccNum, Supplier, currency);

                    //Rest = 0  => Journal Transaction
                    var RestAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;
                    //Rest >0   => Update Supplier Balance , Supplier Transaction, Journal Transaction
                    if (RestAmount > 0)
                    {
                        var LocalAmount = RestAmount * currency.Rate;//المبلغ المتبقي 
                                                                     //--Supplier Payment
                                                                     //1-Update Balance Contact Table
                        var BalanceAfter = _supplierBalanceManager.UpdateSupplierBalance(Supplier, LocalAmount, true);
                        //2-Update Balance With Currency
                        _supplierBalanceManager.UpdateBalanceInCurrency(Supplier.Id, Supplier.SupplierAccNum
                                                                        , vm.ExpenseDetails.CurrencyId, RestAmount, true);
                        // 3-Supplier Transaction
                        _supplierTransactionManager.ExpenseSupplierTransaction(vm, Supplier.SupplierAccNum, TransId, BalanceAfter);
                    }

                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    var mes = ex.Message;
                    transaction.Rollback();
                }
            }


        }
    }
}

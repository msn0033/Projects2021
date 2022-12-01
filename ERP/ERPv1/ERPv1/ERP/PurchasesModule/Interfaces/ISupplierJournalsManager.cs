using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using Microsoft.AspNetCore.Http;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface ISupplierJournalsManager
    {
        string PurchaseJournal(PurchaseContainer vm, Contacts contact, IFormFile Invoice);
        public string SupplierPaymentJournal(SupplierPaymentContainer vm, CRM.Model.Contacts contact, decimal LocalAmount);
        string ExpenseJournal(ExpenseContainerVM vm, string ExpenseAccNum, Contacts contacts, Currency currency);
    }
}
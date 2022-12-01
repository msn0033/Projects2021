using ERPv1.CRM.Model;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ERPv1.ERP.SalesModule.Interfaces
{
    public interface IClientJournalManager
    {
        string IncomeJournal(SalesContainer vm, Contacts contact, IFormFile Invoice);
        string PurchaseJournal(SalesContainer vm, string InvoiceNum);
        //List<JournalDetailsVM> PurchaseJournalDetails(SalesContainer vm, Currency currency, string InvoiceNum);
        //  decimal GetTotal(List<StoreItemBalanceDetails> OrderedList, StoreItem StoreItem, SalesItemDetails item);

        string ClientPaymentJournal(ClientPaymentContainer vm, CRM.Model.Contacts contact, decimal Amount);
    }
}
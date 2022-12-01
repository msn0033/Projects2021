using AutoMapper;
using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.ERPSettings.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class PurchaseManager : IPurchaseManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        private readonly ISupplierJournalsManager _supplierJournalsManager;
        private readonly ISupplierBalanceManager _supplierBalanceManager;
        private readonly ISupplierTransactionManager _supplierTransactionManager;

        //Money Transactions
        //add new Purchase
        //Create Journal
        //Update Balance Contacts &  Balance ContactBalanceInCurrency
        //SupplierTransaction(PurchaseId,TransId)


        //Update StoreItem Balance And QTY
        //StoreTransaction
        //StoreItemDetails
        //SN=>Insert

        public PurchaseManager(ApplicationDbContext db, IMapper mapper, ISupplierJournalsManager supplierJournalsManager, ISupplierBalanceManager supplierBalanceManager, ISupplierTransactionManager supplierTransactionManager)
        {
            _db = db;
            _mapper = mapper;
            _supplierJournalsManager = supplierJournalsManager;
            _supplierBalanceManager = supplierBalanceManager;
            _supplierTransactionManager = supplierTransactionManager;
         
        }
        public void SavePurchase(PurchaseContainer vm ,IFormFile Invoicefile)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    #region ex
                    ////Money Transactions
                    //add new Purchase
                    //Create Journal
                    //Update Balance Contacts &  Balance ContactBalanceInCurrency
                    //SupplierTransaction(PurchaseId,TransId)

                    //
                    //Update StoreItem Balance And QTY
                    //StoreTransaction
                    //StoreItemDetails
                    //SN=>Insert

                    #endregion
                    #region add New Purchase  
                    //add New Purchase
                    //var purchase = new Purchase();
                    //purchase.InvoiceNum = vm.PurchaseSummary.InvoiceNum;
                    //purchase.IsVAT = vm.PurchaseSummary.IsVAT;
                    //purchase.PurchaseDate = vm.PurchaseSummary.PurchaseDate.ConvertDate();
                    //purchase.IsFullyPaid = false;
                    //purchase.Paid = 0;
                    //purchase.SupplierId = vm.SupplierData.SupplierId;
                    //purchase.TotalAmount = vm.PurchaseSummary.TotalAmount;
                    //purchase.TotalAmountWithVAT = vm.PurchaseSummary.TotalAmountWithVAT;
                    //purchase.VATAmount = vm.PurchaseSummary.VATAmount;
                    //==========================================================

                    var purchase = _mapper.Map<Purchase>(vm.PurchaseSummary);
                    purchase.SupplierId = vm.SupplierData.SupplierId;
                    _db.Purchases.Add(purchase);
                    _db.SaveChanges();

                    #endregion

                    var contact = _db.Contacts.Find(vm.SupplierData.SupplierId);

                    #region Create Journal
                    //Create Journal=> 1-StoreAccounts(It is more than one or one  this Products   )  2-Vat-Purchase 3- SupplierAcc
                    /*
                     * تسميع في ارصدة المخزن
                     * تسميع في ارصدة المورد
                     * تسميع في رصيد الضريبة
                     */
                    var TransId = _supplierJournalsManager.PurchaseJournal(vm, contact, Invoicefile);
                    #endregion

                    #region Update Balance Contacts & Update Balance ContactBalanceInCurrency

                    var currency = _db.Currency.Find(vm.PurchaseSummary.CurrencyId);

                    //Get TotalAmount With Local Currency
                    var LocalAmount = vm.PurchaseSummary.TotalAmountWithVAT * currency.Rate;

                    //Update Balance In SupplierBlanceInCurrency
                    _supplierBalanceManager.ManageSupplierBlanceInCurrency
                        (contact.Id, contact.SupplierAccNum, currency.Id, vm.PurchaseSummary.TotalAmountWithVAT, true);

                    //Update Contact Balance
                    var BalanceAfter = _supplierBalanceManager.UpdateSupplierBalance(contact, LocalAmount, true);

                    #endregion

                    #region  SupplierTransaction(PurchaseId,TransId)
                    _supplierTransactionManager.PurchaseSupplierTransaction(vm, purchase.Id, contact.SupplierAccNum, TransId, BalanceAfter);
                    #endregion


                    #region StoreItem

                    var vat = _db.VATs.Find(1).VatRate;//vatRate
                    foreach (var item in vm.PurchasesDetails)
                    {
                        #region Update StoreItem Balance And QTY
                        var total = item.QTY * item.UnitPrice * currency.Rate;
                        var VatAmount = total * vat;
                        var TotalwithVat = total + VatAmount;

                        var TotalStoreItem = vm.PurchaseSummary.IsVAT ? TotalwithVat : total;
                        var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                        StoreItem.Qty += item.QTY;
                        StoreItem.Balance += TotalStoreItem;
                        _db.StoreItems.Update(StoreItem);
                        _db.SaveChanges();
                        #endregion
                        #region Store Transaction
                        var StoreTrans = new StoreTransaction();
                        StoreTrans.StoreItemId = item.StoreItemId;
                        StoreTrans.PurchaseId = purchase.Id;
                        StoreTrans.UnitPrice = item.UnitPrice;

                        StoreTrans.Qty = item.QTY;
                        StoreTrans.StoreTransTypeEnum = StoreTransTypeEnum.Purchase;
                        StoreTrans.QtyBalanceAfter = StoreItem.Qty;
                        _db.StoreTransactions.Add(StoreTrans);
                        _db.SaveChanges();
                        #endregion
                        #region StoreItemBalanceDetails
                        var ItemBalanceDetails = new StoreItemBalanceDetails();
                        ItemBalanceDetails.StoreItemId = item.StoreItemId;
                        ItemBalanceDetails.StoreTransactionId = StoreTrans.Id;
                        ItemBalanceDetails.CurrentBalance = item.QTY;
                        if (!string.IsNullOrEmpty(item.ExpiryDate))
                            ItemBalanceDetails.ExpiryDate = item.ExpiryDate.ConvertDate();
                        _db.StoreItemBalanceDetails.Add(ItemBalanceDetails);
                        _db.SaveChanges();
                        #endregion

                        #region SN=>Insert

                        if (!string.IsNullOrEmpty(item.SN))
                        {
                            var SN = item.SN.Split(',').ToList();
                            foreach (var sn in SN)
                            {
                                var itemWithSN = new StoreItemWithSN()
                                {
                                    SerialNumber = sn,
                                    StoreItemId = item.StoreItemId,
                                    StoreTransactionId = StoreTrans.Id
                                };
                                _db.StoreItemWithSN.Add(itemWithSN);
                                _db.SaveChanges();
                            }
                        }
                        #endregion

                    }
                    #endregion
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

        public PurchaseContainer NewPurchase(int supplierId)
        {
            var vm = new PurchaseContainer();
            var supplier = _db.Contacts.Find(supplierId);
            vm.SupplierData.SupplierId = supplierId;
            vm.SupplierData.SupplierName = supplier.NameAr;
            vm.SupplierData.Phone = supplier.Phone1;
            vm.SupplierData.Balance = supplier.SupplierBalance;

            return vm;
        }
    }
}

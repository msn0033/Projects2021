using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.SalesModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main
{
    [Table("Finance_CurrentAsset_Inventory_Main_StoreTransaction")]

    public class StoreTransaction //حركة المخزن
    {
        public int Id { get; set; }

        //public int RepositoryId { get; set; }//حركة المستودع
        //[ForeignKey("RepositoryId")]
        //public Repository Repository { get; set; }

        public int StoreItemId { get; set; }// حركة الصنف
        [ForeignKey("StoreItemId")]
        public StoreItem StoreItem { get; set; }
        public StoreTransTypeEnum StoreTransTypeEnum { get; set; }// 50شراء10 -بيع20 -رجيع شرا30- رجيع بيع40- مفقود 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; } //الكمية

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }//السعر

        public int? PurchaseId { get; set; }//فاتورة مشتريات لمورد
        [ForeignKey("PurchaseId")]
        public Purchase PurchaseDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal QtyBalanceAfter { get; set; }//الكمية بعد 

        [StringLength(20)]
        public string InvoiceNum { get; set; }// فاتورة مبيعات لعميل
        [ForeignKey("InvoiceNum")]
        public Invoices InvoicesDetails { get; set; }

       
    }
}

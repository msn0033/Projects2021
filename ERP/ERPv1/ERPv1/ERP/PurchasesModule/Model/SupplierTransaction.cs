using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Model
{
    [Table("Finance_Supplier_SupplierTransaction")]

    public class SupplierTransaction
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Contacts SupplierDetails { get; set; }
        public int? PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public Purchase PurchaseDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; }
        [Column(TypeName = "Date")]
        public DateTime PaymentDate { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency CurrencyDetails { get; set; }
        [StringLength(50)]
        public string PaymentAccNum { get; set; }
        [ForeignKey("PaymentAccNum")]
        public AccountChart PaymentAccountDetails { get; set; }
        public SupplierPaymentMethodEnum PaymentMethodEnum { get; set; }
        public string JournalId { get; set; }
        [ForeignKey("JournalId")]
        public Journal  Journal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal  BalanceAfter { get; set; }
    }
}

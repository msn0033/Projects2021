using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Model
{
    [Table("Finance_Sales_Invoices")]

    public class Invoices
    {
        [Key,StringLength(20)]
        public string InvoiceNum { get; set; }//primery Key Num +Year 

        [StringLength(6)]
        public string Year { get; set; }//السنة
        [StringLength(20)]
        public string InvoiceNumYear { get; set; }//رقم الفاتورة

        public int InvoiceCount { get; set; }// Counter
        public int ContactId { get; set; }//العميل
        public int CurrencyId { get; set; }//العملة
        public bool IsVAT { get; set; }//هل يوجد ضريبة


        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }//المبلغ

        [Column(TypeName = "decimal(18,2)")]
        public decimal VATAmount { get; set; }//الضريبة

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWithVAT { get; set; }//مبلغ مع الضريبة

        [Column(TypeName = "Date")]
        public DateTime InvoiceDate { get; set; }//تاريخ الفاتورة
        

        //Mapping

        [ForeignKey("ContactId")]
        public Contacts ContactDetails { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
    }
}

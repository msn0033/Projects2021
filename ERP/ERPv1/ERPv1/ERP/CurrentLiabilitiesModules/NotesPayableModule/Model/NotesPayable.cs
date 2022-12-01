using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model
{
    [Table("Finance_CurrentLiabilties_NP_NotesPayable")]
    public class NotesPayable//اوراق الدفع
    {
        [Key, StringLength(25)]
        public string ChkNum { get; set; }//رقم الشيك

        public DateTime? WritingDate { get; set; }//تاريخ كتابة الشيك

        public DateTime? DueDate { get; set; }//تاريخ استحقاق الشيك


        [Required, Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountLocal { get; set; }//المبلغ بالعملة المحلية


        [Required, Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountForgin { get; set; }//المبلغ بعملة اخرى مثل الدولار

        [Required]
        public int CurrencyId { get; set; }//العملة
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public string BankAccountNum { get; set; }//رقم الحساب البنكي الذي بينخصم منه مبلغ  الشيك- الشيك مرتبط في اي حساب بنك

        [ForeignKey("BankAccountNum")]
        public AccountChart BankAccount { get; set; }

        [StringLength(15)]
        public int SupplierId { get; set; }// المورد الذي سيصرف له الشيك
        [ForeignKey("SupplierId")]
        public Contacts Supplier { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Paid { get; set; }// كم المبلغ المدفوع من الشيك -دفع جزائي 

        public NotesPayableStatusEnum CheckStatus { get; set; }//حالة الشيك
    }
}

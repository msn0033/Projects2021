using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model
{
    [Table("Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory")]
    public class NotesPayableTransactionHistory //تسجيل حركة الشيك-الاحداث التي صارت على الشيك
    {
        // NotesPayableTransactionHistory   تسجيل حركة على اوراق الدفع
        public int Id { get; set; }
        [Required]
        public string ChkNum { get; set; }
        public string TransId { get; set; }

        public DateTime? ActionDate { get; set; } //اكشن معين في تاريخ معين
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PaidAmount { get; set; }//المبلغ المدفوع

        public NotesPayableStatusEnum StatusAfterAction { get; set; }//نوع الاكشن الذي حصل لشيك

        public string Description { get; set; }//الوصف


        [ForeignKey("ChkNum")] //تفاصيل الشيك
        public NotesPayable ChkDetails { get; set; }
    }
}

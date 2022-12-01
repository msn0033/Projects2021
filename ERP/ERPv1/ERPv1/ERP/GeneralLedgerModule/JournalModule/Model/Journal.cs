using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Model
{
    [Table(name: "Finance_GL_Journal")]
    public class Journal//دفتر اليومية
    {
        [Key, Required, StringLength(15)]
        public string JournalId { get; set; }//نظام الترقيم

        [Required]
        //DateTimeOffset بساعة الدقيقة 
        public DateTimeOffset EntryDate { get; set; }//تاريخ كتابة القيد-المعاملة

        [Column(TypeName = "Date")]
        public DateTime TransDate { get; set; }//تاريخ المعاملة

        [Required]
        public string TransDes { get; set; }//وصف العملية

        public string DocName { get; set; }//ملف -صورة

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//incres automatic
        public int TransCount { get; set; }//عداد رقم القيد

        public SystemModulesEnum SystemModules { get; set; }

        public string UserName { get; set; }//اسم المستخدم الذي عمل القيد
    }
}

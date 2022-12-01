using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Model
{
    [Table(name: "Finance_GL_JournalDetails")]
    public class JournalDetails //تفاصيل دفترة اليومية
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string JournalId { get; set; }

        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        [StringLength(15)]
        public string AccNum { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }//سعر بالدولار

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal AmountLocal { get; set; }//سعر بريال السعودي

        public JournalSideEnum Side { get; set; }//مدين او دائن 

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal BalanceAfter { get; set; }

        [Required]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal UsedRate { get; set; }
    }
}

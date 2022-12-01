using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.ERPSettings.Model
{
    [Table(name: "Finance_Settings_Currency")]
    public class Currency//العملة
    {
        public int Id { get; set; }
        [Required, StringLength(25)]
        public string CurrencyName { get; set; }// اسم العملة انجليزي

        [Required, StringLength(25)]
        public string CurrencyNameAr { get; set; }//اسم العملة عربي

        [Required, StringLength(10)]
        public string CurrencyAbbrev { get; set; }//Egy ,SAR,Usd هذااختصار العملة مثلا

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }//معدل الصرف

        public bool IsDefault { get; set; }// اذا كان صح البرنامج يتعامل بهذا العملة 

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.ERPSettings.Model
{
    [Table("Finance_Settings_VAT")]
    public class VAT
    {

        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VatRate  { get; set; }
    }
}

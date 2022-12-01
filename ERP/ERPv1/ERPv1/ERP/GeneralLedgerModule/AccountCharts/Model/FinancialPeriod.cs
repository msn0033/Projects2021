using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    [Table(name: "Finance_GL_FinancialPeriod")]
    public class FinancialPeriod// الفترة المالية-السنة المالية
    {
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string YearName { get; set; }
        public bool IsActive { get; set; }//ماهي السنة النشطة
    }
}

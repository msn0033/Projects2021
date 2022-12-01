using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{

    //الحساب في شقرة الحسابات كان قافل على كام او كان بادي بكام
    //ابتديت في البنك كم
    //ابتديت في الخزنة كم
    // ابتديت في المخزن بكم
    //يعني عند تقفيل الفترة المالية كم كانت حساباتك
    //...etc
    [Table(name: "Finance_GL_HistoricalBalance")]
    public class HistoricalBalance    //يعني عند تقفيل الفترة المالية كم كانت حساباتك
    {
        public int Id { get; set; }
        public int FinancialPeriodId { get; set; }
        [ForeignKey("FinancialPeriodId")]
        public FinancialPeriod FinancialPeriods { get; set; }
        public string AccNum { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}

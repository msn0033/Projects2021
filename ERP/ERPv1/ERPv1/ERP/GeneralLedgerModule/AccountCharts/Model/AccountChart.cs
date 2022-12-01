using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    [Table(name: "Finance_GL_AccountChart")]
    public class AccountChart// شجرة الحسابات

    {
        [Key, StringLength(50)]
        public string AccNum { get; set; }//رقم الحساب

        [Required, StringLength(150)]
        public string AccountName { get; set; }//اسم الحساب

        [Required, StringLength(150)]
        public string AccountNameAr { get; set; }//اسم الحساب عربي

        public AccountNatureEnum AccountNature { get; set; }//طبيعة الحساب credit/debit

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }//الرصيد

        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingBalance { get; set; }//رصيد اول المدة

        public bool IsParent { get; set; }//حساب رئيسي او حساب فرعي 

        [StringLength(50)]
        public string ParentAcNum { get; set; }//رقم الحساب الرئيسي

        [DefaultValue(true)]
        public bool IsActive { get; set; }// الحساب مفعل - غير مفعل

        #region AccountChartCounter
        public int AccTypeId { get; set; }
        [ForeignKey("AccTypeId")]
        public AccountChartCounter AccType { get; set; }

        #endregion
        #region Currency // العملة
        [Required]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        #endregion
        #region Branch //الفرع
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        #endregion
    }

}

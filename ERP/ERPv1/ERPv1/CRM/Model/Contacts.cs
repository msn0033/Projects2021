using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.Model
{
    [Table(name: "CRM_Contacts")]
    public class Contacts
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        [Display(Name = "اسم انجليزي")]
        public string Name { get; set; }

        [Required, StringLength(255)]
        [Display(Name = "اسم عربي")]
        public string NameAr { get; set; }

        [StringLength(50)]
        public string TaxationCard { get; set; }//رقم الضريبي


        [StringLength(50),RegularExpression("^[0-9]*$",ErrorMessage ="ارقام فقط")]
        [Display(Name = "رقم الجوال 1")]
        public string Phone1 { get; set; }


        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "ارقام فقط")]
        [Display(Name = "رقم الجوال 2")]
        public string Phone2 { get; set; }


        [StringLength(70),DataType(DataType.EmailAddress)]
        [Display(Name ="البريد الالكتروني")]
        public string Email { get; set; }

        #region Client
        public bool  IsClient { get; set; }//عميل او مورد
        [StringLength(50)]
        public string ClientAccNum { get; set; }//رقم حساب العميل في شجرة الحسابات
        [ForeignKey("ClientAccNum")]
        public AccountChart ClientAccountDetails { get; set; }//تفاصيل حساب العميل من شجرة الحسابات

        [Display(Name = "رصيد العميل")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal ClientBalance { get; set; }//b2c  رصيد العميل


        #endregion
        #region Supplier
        public bool  IsSupplier { get; set; }//عميل او مورد
        [StringLength(50)]
        public string SupplierAccNum { get; set; }//رقم حساب المورد في شجرة الحسابات
        [ForeignKey("SupplierAccNum")]
        public AccountChart SupplierAccountDetails { get; set; }//تفاصيل حساب المورد من شجرة الحسابات
        [Display(Name = "رصيد المورد")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SupplierBalance { get; set; }// b2c  رصيد المورد

        #endregion

    }
}

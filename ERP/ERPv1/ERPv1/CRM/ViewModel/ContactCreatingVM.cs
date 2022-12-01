using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.ViewModel
{
    public class ContactCreatingVM
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        [Display(Name = "اسم انجليزي")]
        public string Name { get; set; }

        [Required, StringLength(255)]
        [Display(Name = "اسم عربي")]
        public string NameAr { get; set; }

        [Display(Name = "الرقم الضريبي"), StringLength(50)]
        public string TaxationCard { get; set; }//رقم الضريبي

        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "ارقام فقط")]
        [Display(Name = "رقم الجوال 1")]
        public string Phone1 { get; set; }


        [StringLength(50), RegularExpression("^[0-9]*$", ErrorMessage = "ارقام فقط")]
        [Display(Name = "رقم الجوال 2")]
        public string Phone2 { get; set; }


        [StringLength(70), DataType(DataType.EmailAddress)]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        //b2bاذا صح انشاء حساب منفصل 
        // b2c انشاء الحساب حسب اختياري رقم الحساب من شجرة الحسابات   false اذا
        public bool CreateAccount { get; set; }

        public string AccNum { get; set; }
        public int BranchId { get; set; }
        public int CurrencyId { get; set; }

    }
}

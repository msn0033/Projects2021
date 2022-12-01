using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLix.Models
{
    public partial class Company
    {
        [Key]
        public int Com_Id { get; set; }
        [Display(Name ="اسم الشركة")]
        [Required(ErrorMessage ="هنا تكتب الرسالة التي تريدها تظهر للمستخدم")]
        [StringLength(20,MinimumLength =5,ErrorMessage =" الحد الاقصى 20 والحد الادنى 5 حروف")]
        
        public string CName { get; set; }
        public string Ctype { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Curl { get; set; }
        [DisplayFormat(DataFormatString ="{0:d}",ApplyFormatInEditMode =false)]
        public DateTime Edate { get; set; }
        //[RegularExpression("",ErrorMessage =" يجب ادخال حروف وارقام ورموز")]
        public String Password { get; set; }
        [DisplayFormat(DataFormatString ="{0:N}",ApplyFormatInEditMode =false)]
        public int Capital { get; set; }
        public string cLogo { get; set; }
    }
}

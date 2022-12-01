using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ProgLix.Models
{
    public partial class Company
    {
        [Required]
        [Display(Name ="تاكيد الايميل")]
        public string confEmail { get; set; }
        [Required]
        [Display(Name="تاكيد كلمة المرور")]
        public string confPassword { get; set; }
    }
   
    
}

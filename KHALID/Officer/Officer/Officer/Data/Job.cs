using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Officer.Data
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "اسم الوظيفة")]
        public string jobTitle { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفة")]
        public string jobContent { get; set; }

        [Display(Name = "صورة الوظيفة")]
        public string jobImage { get; set; }

        [Display(Name = "نوع الوظيفة")]
        public int CategoryId { get; set; }
        [Display(Name = "نوع الوظيفة")]
        public Category Category { get; set; }
    }
}

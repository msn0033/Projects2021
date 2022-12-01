using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Officer.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = " اسم نوع الوظيفة ")]
        public string categoryName { get; set; }

        [Required]
        [Display(Name ="وصف نوع الوظيفة ")]
        public string categoryDiscription { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}

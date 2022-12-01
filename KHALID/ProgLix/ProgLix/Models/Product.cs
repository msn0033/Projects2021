using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLix.Models
{
    public class Product
    {
        [Key]
        public int ProId { get; set; }
        public string ProName { get; set; }
        public float unitPrice { get; set; }
        public string logo { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int companyId { get; set; }
        public Company company { get; set; }
    }
}

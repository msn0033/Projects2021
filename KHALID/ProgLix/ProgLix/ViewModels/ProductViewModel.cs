using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProgLix.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLix.ViewModels
{
    public class ProductViewModel
    {
        
        public int ProId { get; set; }
        public string ProName { get; set; }
        public float unitPrice { get; set; }
        public string logo { get; set; }

        public int CategoryId { get; set; }
        public List<Category> Category { get; set; }

        public int companyselectId { get; set; }
        public SelectList companyselect { get; set; }

        public IFormFile uploadFile { get; set; }

    }
}

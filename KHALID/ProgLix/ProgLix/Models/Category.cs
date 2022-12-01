using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLix.Models
{
    public class Category
    {
        [Key]
        public int Cat_Id { get; set; }
        public string Cat_Name { get; set; }
    }
}

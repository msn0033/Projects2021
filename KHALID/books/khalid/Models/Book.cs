using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string email { get; set; }
        public Author author { get; set; }
        public string ImageUrl { get; set; }

    }
}

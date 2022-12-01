using khalid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace khalid.ViewModels
{
    public class BookAuthor
    {
        public int BookId { get; set; }

        [Required (ErrorMessage ="رجاء كتابة اسم الكتاب")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "اسم الكتاب من 2 احرف الى 50 حرف")]

        public string title { get; set; }

        [Required(ErrorMessage ="رجاء كتابة وصف الكتاب")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "وصف الكتاب من 5 احرف الى 120 حرف")]
        public string Description { get; set; }

        [EmailAddress(ErrorMessage ="رجاء كتابة الايميل بشكل صحيح")]
        public string email { get; set; }

        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        
        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }
    }
}

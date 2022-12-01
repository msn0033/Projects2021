using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace khalid.Models
{
    public class BookDbContext :DbContext
    {
        public BookDbContext( DbContextOptions<BookDbContext>op):base(op)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

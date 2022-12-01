using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Model
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> option) : base(option)
        {
        }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}

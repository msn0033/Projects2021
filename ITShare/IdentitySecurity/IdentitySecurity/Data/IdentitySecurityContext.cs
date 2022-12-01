using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentitySecurity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentitySecurity.Data
{
    public class IdentitySecurityContext : IdentityDbContext
    {
        public IdentitySecurityContext (DbContextOptions<IdentitySecurityContext> options)
            : base(options)
        {
        }

        public DbSet<IdentitySecurity.Models.Course> Course { get; set; }
    }
}

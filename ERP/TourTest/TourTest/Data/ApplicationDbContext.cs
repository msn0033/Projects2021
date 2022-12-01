using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourTest.Models;
using TourTest.ToursModule.Models;

namespace TourTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //primerKey off table Reservation
            builder.Entity<Reservation>().HasKey(x => new { x.ClientId, x.TourId });
        }
        public DbSet<Client>Clients { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PaymentHistory> paymentHistories { get; set; }
    }
}

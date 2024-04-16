using Courier.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                Company.OurCompany, Company.TheOtherCompany
                );
            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.SourceAddress)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.DestinationAddress)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Receiver)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinookDB.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChinookDB.Data
{
    class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //: base(options)
        //{
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<AppUser>().HasData(new AppUser { Id = 1, Username = "admin", Password = "password" });
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Change to your Location
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Chinook2; Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}

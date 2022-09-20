using System.Collections.Generic;
using AppForMovies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppForMovies.API.Data
{
    public class AppDBContext:DbContext
    {
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseItem> PurchaseItem { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //we may call the fluent API to customize how we map classes to tables
            //https://docs.microsoft.com/es-es/ef/ef6/modeling/code-first/fluent/types-and-properties
            
            base.OnModelCreating(builder);
            //Table Per Hierarchy
            //https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
            builder.Entity<PaymentMethod>()
                .HasDiscriminator<string>("PaymentMethod_type")
                .HasValue<PaymentMethod>("PaymentMethod")
                .HasValue<CreditCard>("PaymentMethod_CreditCard")
                .HasValue<PayPal>("PaymentMethod_PayPal");

            builder.Entity<IdentityUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<IdentityUser>("IdentityUser")
                .HasValue<Administrator>("Administrator")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Customer>("Customer");

            //Alternate Keys
            //https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations
            builder.Entity<Genre>().HasAlternateKey(g => new { g.Name });
            builder.Entity<Movie>().HasAlternateKey(m => new { m.Title });
            builder.Entity<PurchaseItem>().HasAlternateKey(pi => new { pi.MovieId, pi.PurchaseId });

        }
    }
}

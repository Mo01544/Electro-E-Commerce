﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project_ASP.Net.Models
{
    public class ASPContext:IdentityDbContext<ApplicationUser>
    {
        public ASPContext() : base()
        {

        }

        public ASPContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = Asp; Integrated Security = True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasKey(n => new { n.product_id, n.order_id });
            base.OnModelCreating(modelBuilder);
        }
        
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet <Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_ASP.Net.Models
{
    public class ASPContext : IdentityDbContext<ApplicationUser>
    {

        public ASPContext(DbContextOptions options) : base(options)
        {
        }

        public ASPContext()
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

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<FilterPanalCategoryData> CategoriesFilterPanel { get; set; }
    }
}

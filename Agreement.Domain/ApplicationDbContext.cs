using Agreement.Domain.Account;
using Agreement.Domain.Dto;
using Agreement.Domain.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agreement.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<Account.ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Account.ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Agreement.Domain.Product.Product> Products { get; set; }

        public DbSet<Domain.Product.Agreement> Agreements { get; set; }

        public DbSet<AgreementDto> AgreementList { get; set; }
        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductGroup>(entity =>
            {
                entity.HasIndex(e => e.GroupCode).IsUnique();
            });

            builder.Entity<Product.Product>(entity =>
            {
                entity.HasIndex(e => e.ProductNumber).IsUnique();
            });

            builder.Entity<Domain.Product.Agreement>().HasKey(c => new { c.Id });

            builder.Entity<AgreementDto>().HasNoKey();

        }

    }
}

using ApplianceAndElectronicStore.Models;
using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<ProductSpecificationNumberInOrder> ProductSpecificationNumbersInOrder { get; set; }
        public DbSet<ProductSpecificationValue> ProductSpecificationsValues { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.Name, c.NameForUrl })
                .IsUnique();

            modelBuilder.Entity<Manufacturer>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<ProductSpecification>()
                .HasIndex(ps => ps.Name)
                .IsUnique();

            modelBuilder.Entity<ProductSpecificationValue>()
                .HasIndex(psv => new { psv.ProductId, psv.ProductSpecificationId })
                .IsUnique();

            modelBuilder.Entity<ProductSpecificationNumberInOrder>()
                .HasIndex(n => new { n.CategoryId, n.ProductSpecificationId })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.ProductModel })
                .IsUnique();

            modelBuilder.Entity<CategoryProductSpecification>(builder => {
                builder.HasKey(cps => new {
                    cps.CategoryId,
                    cps.ProductSpecificationId
                });

                builder.HasOne(cps => cps.Category)
                       .WithMany(c => c.CategoriesProductSpecifications);

                builder.HasOne(cps => cps.ProductSpecification)
                       .WithMany(c => c.CategoriesProductSpecifications);

                builder.ToTable("CategoriesProductSpecifications");
            });

            modelBuilder.Entity<Order>(builder => {
                builder.Property(o => o.CustomerId)
                    .HasColumnName("AspNetUserId")
                    .IsRequired();

                builder.Property(o => o.TotalSum)
                    .IsRequired();
            });

            modelBuilder.Entity<Customer>(builder => {
                builder.Property(c => c.ApplicationUserId)
                    .HasColumnName("AspNetUserId");

                builder.HasKey(c => c.ApplicationUserId);

                builder.HasOne(c => c.ApplicationUser)
                    .WithOne()
                    .HasForeignKey<Customer>(c => c.ApplicationUserId)
                    .IsRequired();
            });
        }
    }
}

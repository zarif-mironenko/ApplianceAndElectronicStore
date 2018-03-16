﻿// <auto-generated />
using System;
using ApplianceAndElectronicStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace ApplianceAndElectronicStore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180313102906_IntitalCreate")]
    partial class IntitalCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NameForUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name", "NameForUrl")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.CategoryProductSpecification", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("ProductSpecificationId");

                    b.HasKey("CategoryId", "ProductSpecificationId");

                    b.HasIndex("ProductSpecificationId");

                    b.ToTable("CategoriesProductSpecifications");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Customer", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnName("AspNetUserId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ApplicationUserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnName("AspNetUserId");

                    b.Property<string>("DeliveryMethod")
                        .IsRequired();

                    b.Property<string>("PaymentMethod")
                        .IsRequired();

                    b.Property<DateTime>("PlacementDateTime");

                    b.Property<string>("Region")
                        .IsRequired();

                    b.Property<bool>("Shipped");

                    b.Property<int>("TotalSum");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NumberInOrder");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("ProductQuantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImagePath");

                    b.Property<int>("ManufacturerId");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.Property<string>("ProductModel")
                        .IsRequired();

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name", "ProductModel")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.ProductSpecification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProductSpecifications");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.ProductSpecificationNumberInOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("NumberInOrder");

                    b.Property<int>("ProductSpecificationId");

                    b.HasKey("Id");

                    b.HasIndex("ProductSpecificationId");

                    b.HasIndex("CategoryId", "ProductSpecificationId")
                        .IsUnique();

                    b.ToTable("ProductSpecificationNumbersInOrder");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.ProductSpecificationValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<int>("ProductSpecificationId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ProductSpecificationId");

                    b.HasIndex("ProductId", "ProductSpecificationId")
                        .IsUnique();

                    b.ToTable("ProductSpecificationsValues");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.CategoryProductSpecification", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Category", "Category")
                        .WithMany("CategoriesProductSpecifications")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.ProductSpecification", "ProductSpecification")
                        .WithMany("CategoriesProductSpecifications")
                        .HasForeignKey("ProductSpecificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Customer", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.ApplicationUser", "ApplicationUser")
                        .WithOne()
                        .HasForeignKey("ApplianceAndElectronicStore.Models.Entities.Customer", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Order", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.OrderItem", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.Product", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.ProductSpecificationNumberInOrder", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Category", "Category")
                        .WithMany("ProductSpecificationNumbersInOrder")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.ProductSpecification", "ProductSpecification")
                        .WithMany("ProductSpecificationNumbersInOrder")
                        .HasForeignKey("ProductSpecificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplianceAndElectronicStore.Models.Entities.ProductSpecificationValue", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.Product", "Product")
                        .WithMany("ProductSpecificationValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.Entities.ProductSpecification", "ProductSpecification")
                        .WithMany("ProductSpecificationValues")
                        .HasForeignKey("ProductSpecificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplianceAndElectronicStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApplianceAndElectronicStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
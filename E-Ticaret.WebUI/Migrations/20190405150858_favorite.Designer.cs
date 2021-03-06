﻿// <auto-generated />
using ETicaret.WebUI.Repository.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ETicaret.WebUI.Migrations
{
    [DbContext(typeof(ETicaretContext))]
    [Migration("20190405150858_favorite")]
    partial class favorite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ETicaret.WebUI.Entity.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<DateTime>("SaveDate");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("TotalPrice");

                    b.Property<string>("UserName");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Image");

                    b.Property<bool>("IsApproved");

                    b.Property<decimal>("Price");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductName");

                    b.Property<decimal?>("SalePrice");

                    b.Property<DateTime>("SaveDate");

                    b.Property<int>("Stock");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("ProductId");

                    b.Property<double>("Sale");

                    b.Property<DateTime>("StartingDate");

                    b.HasKey("PromotionId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Slider", b =>
                {
                    b.Property<int>("SliderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<string>("SliderDescription");

                    b.Property<string>("SliderLink");

                    b.Property<string>("SliderName");

                    b.HasKey("SliderId");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Order", b =>
                {
                    b.HasOne("ETicaret.WebUI.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Product", b =>
                {
                    b.HasOne("ETicaret.WebUI.Entity.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ETicaret.WebUI.Entity.Promotion", b =>
                {
                    b.HasOne("ETicaret.WebUI.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("ETicaret.WebUI.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using CodeCanvas.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeCanvas.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221019175550_walletseeding")]
    partial class walletseeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("CodeCanvas.Entities.CurrencyRateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Rate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CurrencyRates");
                });

            modelBuilder.Entity("CodeCanvas.Entities.WalletEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 0m,
                            CreatedAt = new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Local),
                            CurrencyCode = "USD",
                            UpdatedAt = new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

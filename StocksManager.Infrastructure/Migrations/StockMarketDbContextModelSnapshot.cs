﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StocksManager.Infrastructure.Migrations
{
    [DbContext(typeof(StockMarketDbContext))]
    partial class StockMarketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.BuyOrder", b =>
                {
                    b.Property<Guid>("BuyOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAndTimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("BuyOrderID");

                    b.ToTable("BuyOrders", (string)null);

                    b.HasData(
                        new
                        {
                            BuyOrderID = new Guid("9335b076-4a48-4d8f-8cd5-3a7a6b9b0319"),
                            DateAndTimeOfOrder = new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 80610.01m,
                            Quantity = 20L,
                            StockName = "Quad Graphics, Inc",
                            StockSymbol = "QUAD"
                        },
                        new
                        {
                            BuyOrderID = new Guid("4333d9fa-4bdd-4f2c-8852-90240d66c418"),
                            DateAndTimeOfOrder = new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 46607.8m,
                            Quantity = 30L,
                            StockName = "Teradyne, Inc.",
                            StockSymbol = "TER"
                        },
                        new
                        {
                            BuyOrderID = new Guid("55b3cbbf-7a75-4c5d-b5c6-be92b47a2808"),
                            DateAndTimeOfOrder = new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 458.75m,
                            Quantity = 15L,
                            StockName = "Church & Dwight Company, Inc.",
                            StockSymbol = "CHD"
                        });
                });

            modelBuilder.Entity("Entities.SellOrder", b =>
                {
                    b.Property<Guid>("SellOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAndTimeOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SellOrderID");

                    b.ToTable("SellOrders", (string)null);

                    b.HasData(
                        new
                        {
                            SellOrderID = new Guid("83cb4c97-0164-4c1e-ae6a-5f1e8cc4b2e3"),
                            DateAndTimeOfOrder = new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 876.3m,
                            Quantity = 40L,
                            StockName = "IHS Markit Ltd.",
                            StockSymbol = "INFO"
                        },
                        new
                        {
                            SellOrderID = new Guid("de60476c-e07e-42b9-8f0f-3170d3053fab"),
                            DateAndTimeOfOrder = new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 563.32m,
                            Quantity = 30L,
                            StockName = "GasLog LP.",
                            StockSymbol = "GLOG^A"
                        },
                        new
                        {
                            SellOrderID = new Guid("24196885-fdf1-40b9-9f42-fcaf5b56c9b7"),
                            DateAndTimeOfOrder = new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified),
                            Price = 91.61m,
                            Quantity = 309L,
                            StockName = "VictoryShares US Large Cap High Div Volatility Wtd ETF",
                            StockSymbol = "CDL"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

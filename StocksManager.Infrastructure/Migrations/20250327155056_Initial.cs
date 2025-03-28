using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StocksManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    BuyOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.BuyOrderID);
                });

            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.SellOrderID);
                });

            migrationBuilder.InsertData(
                table: "BuyOrders",
                columns: new[] { "BuyOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("4333d9fa-4bdd-4f2c-8852-90240d66c418"), new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 46607.8m, 30L, "Teradyne, Inc.", "TER" },
                    { new Guid("55b3cbbf-7a75-4c5d-b5c6-be92b47a2808"), new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 458.75m, 15L, "Church & Dwight Company, Inc.", "CHD" },
                    { new Guid("9335b076-4a48-4d8f-8cd5-3a7a6b9b0319"), new DateTime(2000, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 80610.01m, 20L, "Quad Graphics, Inc", "QUAD" }
                });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "SellOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("24196885-fdf1-40b9-9f42-fcaf5b56c9b7"), new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 91.61m, 309L, "VictoryShares US Large Cap High Div Volatility Wtd ETF", "CDL" },
                    { new Guid("83cb4c97-0164-4c1e-ae6a-5f1e8cc4b2e3"), new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 876.3m, 40L, "IHS Markit Ltd.", "INFO" },
                    { new Guid("de60476c-e07e-42b9-8f0f-3170d3053fab"), new DateTime(2010, 1, 5, 14, 22, 0, 0, DateTimeKind.Unspecified), 563.32m, 30L, "GasLog LP.", "GLOG^A" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrders");
        }
    }
}

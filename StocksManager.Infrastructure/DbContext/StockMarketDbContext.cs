using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BuyOrder> BuyOrders { get; set; }
       public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");

            //Seed data for BuyOrders
            string buyOrdersJson = System.IO.File.ReadAllText("buyOrders.json");
            List<BuyOrder> buyOrders = System.Text.Json.JsonSerializer.Deserialize<List<BuyOrder>>(buyOrdersJson);

            foreach (BuyOrder buyOrder in buyOrders)
            {
                modelBuilder.Entity<BuyOrder>().HasData(buyOrder);
            }
            //Seed data for SellOrders

            string sellOrdersJson = System.IO.File.ReadAllText("sellOrders.json");
            List<SellOrder> sellOrders = System.Text.Json.JsonSerializer.Deserialize<List<SellOrder>>(sellOrdersJson);

            foreach (SellOrder sellOrder in sellOrders)
            {
                modelBuilder.Entity<SellOrder>().HasData(sellOrder);
            }


        }
    }
    
}

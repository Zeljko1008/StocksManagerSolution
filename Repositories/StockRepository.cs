using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StockRepository : IStockRepository
    {

        private readonly StockMarketDbContext _context;

        public StockRepository(StockMarketDbContext context)
        {
            _context = context;
        }
        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _context.BuyOrders.Add(buyOrder);
            await _context.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _context.SellOrders.Add(sellOrder);
            await _context.SaveChangesAsync();

            return sellOrder;


        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _context.BuyOrders.
                  OrderByDescending(temp => temp.DateAndTimeOfOrder)
                  .ToListAsync();
            return buyOrders;

        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _context.SellOrders
                .OrderByDescending(temp => temp.DateAndTimeOfOrder)
                .ToListAsync();
            return sellOrders;
        }
    }
}

using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public class StocksServiceGetters : IStockServiceGetters
    {
       
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StocksServiceCreateOrder> _logger;

        public StocksServiceGetters(IStockRepository stockRepository, ILogger<StocksServiceCreateOrder> logger ) 
        {
          
            _stockRepository = stockRepository;
            _logger = logger;
        }
       

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            //logging
            _logger.LogInformation("GetBuyOrders called");

            List<BuyOrder> buyOrders = await _stockRepository.GetBuyOrders();   
               
            return buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            //loging 
            _logger.LogInformation("GetSellOrders called");
            List<SellOrder> sellOrders = await _stockRepository.GetSellOrders();
            return sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
        }
    }
}

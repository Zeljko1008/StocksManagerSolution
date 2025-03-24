using Entities;
using Microsoft.EntityFrameworkCore;
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
    
    public class StocksService : IStockService
    {
       
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StocksService> _logger;

        public StocksService(IStockRepository stockRepository, ILogger<StocksService> logger ) 
        {
          
            _stockRepository = stockRepository;
            _logger = logger;
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //logging
            _logger.LogInformation("CreateBuyOrder called");
         


            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            //Model validation 
            ValidationHelper.ModelValidation(buyOrderRequest);

            //convert BuyOrderRequest to BuyOrderResponse
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            //generate BuyOrderID
            buyOrder.BuyOrderID = Guid.NewGuid();

           BuyOrder buyOrderfromRepo = await _stockRepository.CreateBuyOrder(buyOrder);

           return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            //logging
            _logger.LogInformation("CreateSellOrder called");
            _logger.LogInformation($"[CreateSellOrder] DateAndTimeOfOrder pre validacije: {sellOrderRequest.DateAndTimeOfOrder}");

            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            //Model validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            //convert SellOrderRequest to SellOrder
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            SellOrder sellOrderFromRepo =await _stockRepository.CreateSellOrder(sellOrder);
            return sellOrder.ToSellOrderResponse();
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

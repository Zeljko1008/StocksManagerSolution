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
    
    public class StocksServiceCreateOrder : IStockServiceCreateOrder
    {
       
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StocksServiceCreateOrder> _logger;

        public StocksServiceCreateOrder(IStockRepository stockRepository, ILogger<StocksServiceCreateOrder> logger ) 
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
           

            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            //logging

            _logger.LogInformation($"[CreateSellOrder] DateAndTimeOfOrder pre validacije: {sellOrderRequest.DateAndTimeOfOrder}");
            //Model validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            //convert SellOrderRequest to SellOrder
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            SellOrder sellOrderFromRepo =await _stockRepository.CreateSellOrder(sellOrder);
            return sellOrder.ToSellOrderResponse();
        }

       
    }
}

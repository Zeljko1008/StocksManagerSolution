using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the buissness logic for manipulating the stock data
    /// </summary>
    public interface IStockService
    {

        /// <summary>
        /// Adds a new Buy Order request to the database
        /// </summary>
        /// <param name="buyOrderRequest">order object to create</param>
        /// <returns>returns order object (including BuyOrderID)</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Adds buy order response 
        /// </summary>
        /// <param name="sellOrderRequest">Sell order object</param>
        /// <returns>object to sell</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// List of all buy orders
        /// </summary>
        /// <returns>Returns a BuyOrder objects as list</returns>
       Task<List<BuyOrderResponse>> GetBuyOrders();

        /// <summary>
        /// List of all sell orders
        /// </summary>
        /// <returns>Returns a SellOrder objects as list</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}

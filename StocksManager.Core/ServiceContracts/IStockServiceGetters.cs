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
    public interface IStockServiceGetters
    {

       

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

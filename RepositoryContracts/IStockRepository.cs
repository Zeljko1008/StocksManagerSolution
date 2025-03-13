using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents data access methods for Stock entity
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrder">Buy order object</param>
        /// <returns>returns BuyOrder object after creating one</returns>
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrder">Sell order object</param>
        /// <returns>returns SellOrder object after creatin one</returns>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// Gets all buy orders
        /// </summary>
        /// <returns>returns a list of object BuyOrders</returns>
        Task<List<BuyOrder>> GetBuyOrders();

        /// <summary>
        /// Gets all sell orders
        /// </summary>
        /// <returns>returns a list of object SellOrders</returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}

using Entities;
using ServiceContracts.CustomValidators;
using System.ComponentModel.DataAnnotations;
using static ServiceContracts.DTO.IOrderResponsePdf;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest :IOrderRequest
    {
        /// <summary>
        /// DTO class for adding a new Sell Order
        /// </summary>
        [Required(ErrorMessage = "Stock Symbol is required")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Stock Name is required")]
        public string? StockName { get; set; }

        [MinimumYearValidator]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 99999, ErrorMessage = "{0} shuold be between {1} and {2}")]
        public uint Quantity { get; set; }

       

        [Range(1, 99999.99, ErrorMessage = "{0} shuold be between {1} and {2}")]
        public decimal Price { get; set; }
        public OrderType TypeOfOrder => OrderType.SellOrder;

        public decimal TradeAmount { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
        public override string ToString()
        {
            return $"StockSymbol: {StockSymbol}," +
                $" StockName: {StockName}, DateAndTimeOfOrder: {DateAndTimeOfOrder.ToString("dd MMM yyyy")}," +
                $" Quantity: {Quantity}, Price: {Price}";
        }

    }
}


using Entities;
using static ServiceContracts.DTO.IOrderResponsePdf;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse : IOrderResponse
    {
        /// <summary>
        /// DTO class that is used as return type for the buy order response
        /// </summary>
        public Guid? BuyOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderType TypeOfOrder => OrderType.BuyOrder;
        public decimal TradeAmount { get; set; }

        public override string ToString()
        {
            return $"BuyOrderID: {BuyOrderID}, StockSymbol: {StockSymbol}," +
                $" StockName: {StockName}, DateAndTimeOfOrder: {DateAndTimeOfOrder.ToString("dd MMM yyyy")}," +
                $" Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(BuyOrderResponse)) return false;
            BuyOrderResponse buy_order_to_compare = (BuyOrderResponse)obj;
            return
                BuyOrderID == buy_order_to_compare.BuyOrderID &&
                StockSymbol == buy_order_to_compare.StockSymbol &&
                StockName == buy_order_to_compare.StockName &&
                DateAndTimeOfOrder == buy_order_to_compare.DateAndTimeOfOrder &&
                Quantity == buy_order_to_compare.Quantity &&
                Price == buy_order_to_compare.Price &&
                TradeAmount == buy_order_to_compare.TradeAmount ;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
    public static class BuyOrderResponseExtension
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price
            };
        }
    }
}


using Entities;
using static ServiceContracts.DTO.IOrderResponsePdf;
namespace ServiceContracts.DTO
    
{
    public class SellOrderResponse :IOrderResponse
    {
        /// <summary>
        /// DTO class that is used as return type for the sell order response
        /// </summary>
        public Guid SellOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderType TypeOfOrder => OrderType.SellOrder;
        public decimal TradeAmount { get; set; }

        public override string ToString()
        {
            return $"SellOrderID: {SellOrderID}, StockSymbol: {StockSymbol}," +
                $" StockName: {StockName}, DateAndTimeOfOrder: {DateAndTimeOfOrder.ToString("dd MMM yyyy")}," +
                $" Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SellOrderResponse)) return false;
            SellOrderResponse sell_order_to_compare = (SellOrderResponse)obj;
            return
                SellOrderID == sell_order_to_compare.SellOrderID &&
                StockSymbol == sell_order_to_compare.StockSymbol &&
                StockName == sell_order_to_compare.StockName &&
                DateAndTimeOfOrder == sell_order_to_compare.DateAndTimeOfOrder &&
                Quantity == sell_order_to_compare.Quantity &&
                Price == sell_order_to_compare.Price &&
                TradeAmount == sell_order_to_compare.TradeAmount;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class SellOrderResponseExtensions
    {

        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }

    
}


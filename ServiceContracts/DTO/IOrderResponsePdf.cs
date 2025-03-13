using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
   public interface IOrderResponsePdf
    {
       
        public string StockSymbol { get; set; }

        public string StockName { get; set; }

        public DateTime? DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public decimal Price { get; set; }

       public OrderType TypeOfOrder { get;  }

        public decimal TradeAmount { get; set; }

        public enum OrderType
        {
            BuyOrder,
            SellOrder
        }
    }
}

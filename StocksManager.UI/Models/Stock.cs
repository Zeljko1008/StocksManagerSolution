namespace StocksManagerSolution.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(Stock))
            {
                return false;
            }
            Stock stock_to_compare = (Stock)obj;
            return StockSymbol == stock_to_compare.StockSymbol &&
                StockName == stock_to_compare.StockName;
        }
    }
}

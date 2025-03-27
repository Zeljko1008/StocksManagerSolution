namespace ServiceContracts
{
    public interface IFinnHubStockPriceQuoteService
    {
       Task<Dictionary<string,object>?> GetStockPriceQuote(string stockSymbol);
       
    }
}

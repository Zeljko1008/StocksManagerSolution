namespace ServiceContracts
{
    public interface IFinnHubSearchStockService
    {
      

        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}

namespace ServiceContracts
{
    public interface IFinnHubGetStocksService
    {
     

        Task<List<Dictionary<string, string>>?> GetStocks();

      
    }
}

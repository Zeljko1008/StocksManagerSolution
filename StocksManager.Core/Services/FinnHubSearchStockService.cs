
using Exceptions;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnHubSearchStockService : IFinnHubSearchStockService
    {

        private readonly IFinnHubRepository _finnhubRepository;

        public FinnHubSearchStockService(IFinnHubRepository finnHubRepository)
        {

            _finnhubRepository = finnHubRepository;
        }
        

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            try
            {
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);


                return responseDictionary;
            }
            catch (Exception ex)
            {
                FinnhubException finnhubException = new FinnhubException("Error in Finnhub connection for SearchStocks", ex);
                throw finnhubException;
            }

        }
    }
}


using Exceptions;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnHubGetStocksService : IFinnHubGetStocksService
    {

        private readonly IFinnHubRepository _finnhubRepository;

        public FinnHubGetStocksService(IFinnHubRepository finnHubRepository)
        {

            _finnhubRepository = finnHubRepository;
        }
       
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();


                return responseDictionary;
            }
            catch (Exception ex)
            {
                FinnhubException finnhubException = new FinnhubException("Error in Finnhub connection for GetStocks");
                throw finnhubException;
            }
        }

       
    }
}

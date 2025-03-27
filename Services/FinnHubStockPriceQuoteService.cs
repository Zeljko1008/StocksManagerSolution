
using Exceptions;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnHubStockPriceQuoteService : IFinnHubStockPriceQuoteService
    {

        private readonly IFinnHubRepository _finnhubRepository;

        public FinnHubStockPriceQuoteService(IFinnHubRepository finnHubRepository)
        {

            _finnhubRepository = finnHubRepository;
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            try
            {
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);


                return responseDictionary;
            }
            catch (Exception ex)
            {
                FinnhubException finnhubException = new FinnhubException("Error in Finnhub connection for GetStockPriceQuote", ex);
                throw finnhubException;
            }

        }


     
    }
}

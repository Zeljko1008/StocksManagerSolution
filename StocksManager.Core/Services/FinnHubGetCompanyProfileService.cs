
using Exceptions;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnHubGetCompanyProfileService : IFinnHubGetCompanyProfileService
    {

        private readonly IFinnHubRepository _finnhubRepository;

        public FinnHubGetCompanyProfileService(IFinnHubRepository finnHubRepository)
        {

            _finnhubRepository = finnHubRepository;
        }
      


        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            try
            {
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

                //return response dictionary back to the caller
                return responseDictionary;
            }
            catch (Exception ex)
            {
                FinnhubException finnhubException = new FinnhubException("Error in Finnhub connection for GetCompanyProfile", ex);
                throw finnhubException;
            }


        }

    }
}

using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnHubService : IFinnHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IFinnHubRepository _finnHubRepository;

        public FinnHubService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IFinnHubRepository finnHubRepository)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _finnHubRepository = finnHubRepository; 
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}" +
                $"&token={_configuration["FinnHubToken"]}"),
                Method = HttpMethod.Get
            };
            HttpResponseMessage httpResponseMessage =await httpClient.SendAsync(httpRequestMessage);
            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(stream);
            string response = streamReader.ReadToEnd();
            Dictionary<string, object>? responseDictionary =
            JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            if (responseDictionary == null)
            {
                throw new InvalidOperationException("No response from Finnhub server.");
            }
            if (responseDictionary.ContainsKey("error"))
            {
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
            }
            return responseDictionary;


        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            //Create a new HttpClient object
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //create a new HttpRequestMessage object
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}" +
                $"&token={_configuration["FinnHubToken"]}"),
                Method = HttpMethod.Get
            };
            //send the request to the server
            HttpResponseMessage httpResponseMessage =await httpClient.SendAsync(httpRequestMessage);
            Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            StreamReader streamReader = new StreamReader(stream);
            string response =await streamReader.ReadToEndAsync();
            Dictionary<string, object>? responseDictionary =
                JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            if (responseDictionary == null)
            {
                throw new InvalidOperationException("No response from Finnhub server.");
            }
            if (responseDictionary.ContainsKey("error"))
            {
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
            }
            return responseDictionary;

        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
           List<Dictionary<string, string>>? respondeDictionary= await _finnHubRepository.GetStocks();
            return respondeDictionary;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
           Dictionary<string, object>? responseDictionary =  await _finnHubRepository.SearchStocks(stockSymbolToSearch);
            return responseDictionary;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace StocksAppConfigAssignment.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnHubService _finnhubService;
        private readonly IStockService _stockService;
        private readonly StockOptions _stockOptions;
        private readonly IConfiguration _configuration;



        public SelectedStockViewComponent(IFinnHubService finnhubService, IStockService stockService, IOptions<StockOptions> stockOptions, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _stockService = stockService;
            _stockOptions = stockOptions.Value;
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {

            Dictionary<string, object>? companyProfileDictionary = null;

            if (stockSymbol != null)
            {
               
                companyProfileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);
             var stockPriceDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);
                if (companyProfileDictionary != null && stockPriceDictionary != null)
                {
                    Console.WriteLine($"Stock found: {companyProfileDictionary["name"]}");
                    companyProfileDictionary.Add("price", stockPriceDictionary["c"]);
                }
            }

            if (companyProfileDictionary != null && companyProfileDictionary.ContainsKey("logo"))
                return View(companyProfileDictionary);
            else
                return Content("");
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace StocksAppConfigAssignment.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnHubGetCompanyProfileService _finnHubGetCompanyProfileService;
        private readonly IFinnHubStockPriceQuoteService _finnHubStockPriceQuoteService;
        private readonly IStockServiceCreateOrder _stockService;
        private readonly StockOptions _stockOptions;
        private readonly IConfiguration _configuration;



        public SelectedStockViewComponent(IFinnHubGetCompanyProfileService finnHubGetCompanyProfileService,IFinnHubStockPriceQuoteService finnHubStockPriceQuoteService, IStockServiceCreateOrder stockService, IOptions<StockOptions> stockOptions, IConfiguration configuration)
        {
            _finnHubGetCompanyProfileService = finnHubGetCompanyProfileService;
            _finnHubStockPriceQuoteService = finnHubStockPriceQuoteService;
            _stockService = stockService;
            _stockOptions = stockOptions.Value;
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {

            Dictionary<string, object>? companyProfileDictionary = null;

            if (stockSymbol != null)
            {
               
                companyProfileDictionary = await _finnHubGetCompanyProfileService.GetCompanyProfile(stockSymbol);
             var stockPriceDictionary = await _finnHubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);
                if (companyProfileDictionary != null && stockPriceDictionary != null)
                {

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


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksAppConfigAssignment.Models;

namespace StocksAppConfigAssignment.Controllers
{
    [Route("[controller]")]
    public class StockController : Controller
    {
        private readonly StockOptions _stockOptions;
        private readonly IFinnHubService _finnHubService;

        public StockController(IOptions<StockOptions> stockOptions, IFinnHubService finnHubService) 
        { 
            _stockOptions= stockOptions.Value;
            _finnHubService = finnHubService;

        }

        [Route("/")]
        [Route("[action]/{stock?}")]
       
        public async Task<IActionResult> Explore(string? stock, bool showAll=false)
        {
            //get company profile from server

            List<Dictionary<string, string>>? stockDictionary = await _finnHubService.GetStocks();

            List<Stock> stocks = new List<Stock>();

            if(stockDictionary != null)
            {
                if(!showAll && _stockOptions.Top25PopularStocks !=null)
                {
                    string[]? Top25PopularStockList = _stockOptions.Top25PopularStocks.Split(",");
                    if(Top25PopularStockList!=null)
                    {
                        stockDictionary = stockDictionary.Where(temp => Top25PopularStockList.Contains(temp["symbol"])).ToList();
                    }
                }

                stocks = stockDictionary.Select(temp => new Stock()
                {
                    StockSymbol = temp["symbol"],
                    StockName = temp["description"],
                }).ToList();
            }
            ViewBag.stock = stock;
            Console.WriteLine("Stock symbol received in Explore: " + stock);
            ViewBag.currentUrl = HttpContext.Request.Path;
            return View(stocks);

        }
    }
}

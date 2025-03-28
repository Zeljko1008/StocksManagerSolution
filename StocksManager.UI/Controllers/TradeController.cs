using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StocksAppConfigAssignment.Filters.ActionFilters;
using StocksAppConfigAssignment.Models;
using System.Collections.Generic;


namespace StocksAppConfigAssignment.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
       
        private readonly IFinnHubStockPriceQuoteService _finnHubStockPriceQuoteService;
        private readonly IFinnHubGetCompanyProfileService _finnHubGetCompanyProfileService;
        private readonly StockOptions _stockOptions;
        private readonly IConfiguration _configuration;
        private readonly IStockServiceCreateOrder _stockServiceCreateOrder;
        private readonly IStockServiceGetters _stockServiceGetters;
        public TradeController(IFinnHubStockPriceQuoteService finnHubStockPriceQuoteService, IFinnHubGetCompanyProfileService finnHubGetCompanyProfileService ,IOptions<StockOptions>stockOptions, IConfiguration configuration, IStockServiceCreateOrder stocksServiceCreateOrder,IStockServiceGetters stockServiceGetters)
        {
            _finnHubStockPriceQuoteService = finnHubStockPriceQuoteService;
            _finnHubGetCompanyProfileService = finnHubGetCompanyProfileService;
            _stockOptions = stockOptions.Value;
            _configuration = configuration;
            _stockServiceCreateOrder = stocksServiceCreateOrder;
            _stockServiceGetters = stockServiceGetters;
        }
        [Route("[action]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            if(string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }
            Dictionary<string,object>? responseStockQuote= await _finnHubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);
            Dictionary<string, object>? responseCompanyProfile =  await _finnHubGetCompanyProfileService.GetCompanyProfile(stockSymbol);

            StockTrade stock = new StockTrade()
            {
                StockSymbol = stockSymbol,
                
            };
            if(responseCompanyProfile != null&& responseStockQuote !=null)
            {
                stock = new StockTrade()
                {
                   StockSymbol = responseCompanyProfile["ticker"].ToString(),
                    StockName = responseCompanyProfile["name"].ToString(),
                    Quantity = _stockOptions.DefaultOrderQuantity ?? 100,
                    Price = Convert.ToDouble(responseStockQuote["c"].ToString())
                };
            }
            ViewBag.FinnHubToken = _configuration["FinnHubToken"];
            ViewBag.currentUrl = HttpContext.Request.Path;
            return View(stock);
        }

        [HttpPost]
        [Route("[action]")]
        [TypeFilter(typeof(BuyAndSellOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
           
         BuyOrderResponse buyOrderResponse = await _stockServiceCreateOrder.CreateBuyOrder(orderRequest);

         
           
            return RedirectToAction(nameof(Orders));
        }

        [HttpPost]
        [Route("[action]")]
        [TypeFilter(typeof(BuyAndSellOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
           
            SellOrderResponse sellOrderResponse =await _stockServiceCreateOrder.CreateSellOrder(orderRequest);
            

            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            ViewBag.currentUrl = HttpContext.Request.Path;
            ViewBag.ActionName = ControllerContext.ActionDescriptor.ActionName;

            List<BuyOrderResponse> buyOrdersResponse = await _stockServiceGetters.GetBuyOrders();
            List<SellOrderResponse> sellOrdersResponse = await _stockServiceGetters.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrdersResponse ?? new List<BuyOrderResponse>(),
                SellOrders = sellOrdersResponse ?? new List<SellOrderResponse>()
            };

            ViewBag.StockOption = _stockOptions;
            return View(orders);
            
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> OrdersToPDF()
        {
            List<IOrderResponse> orders = new List<IOrderResponse>();
            orders.AddRange(await _stockServiceGetters.GetBuyOrders());
            orders.AddRange(await _stockServiceGetters.GetSellOrders());

            orders = orders.OrderByDescending(o => o.DateAndTimeOfOrder).ToList();

            ViewBag.StockOption = _stockOptions;

            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Bottom = 20,
                    Left = 20,
                    Right = 20,
                    Top = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
            };

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using StocksAppConfigAssignment.Models;
using System.Collections.Generic;


namespace StocksAppConfigAssignment.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IFinnHubService _finnhubService;
        private readonly StockOptions _stockOptions;
        private readonly IConfiguration _configuration;
        private readonly IStockService _stocksService;
        public TradeController(IFinnHubService finnhubService, IOptions<StockOptions>stockOptions, IConfiguration configuration, IStockService stocksService)
        {
           _finnhubService = finnhubService;
            _stockOptions = stockOptions.Value;
            _configuration = configuration;
            _stocksService = stocksService;
        }
        [Route("[action]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            if(string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }
            Dictionary<string,object>? responseStockQuote= await _finnhubService.GetStockPriceQuote(stockSymbol);
            Dictionary<string, object>? responseCompanyProfile =  await _finnhubService.GetCompanyProfile(stockSymbol);

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
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            ViewBag.currentUrl = HttpContext.Request.Path;
            //Set the DateAndTimeOfOrder to the current date and time
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            //re-validate the model
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stock = new StockTrade()
                {
                    StockSymbol = buyOrderRequest.StockSymbol,
                    StockName = buyOrderRequest.StockName,
                    Quantity = buyOrderRequest.Quantity
                };

                return View("Index", stock);
            }
            BuyOrderResponse buyOrderResponse =await _stocksService.CreateBuyOrder(buyOrderRequest);
            List<BuyOrderResponse> buyOrdersResponse = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrdersResponse = await _stocksService.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrdersResponse ?? new List<BuyOrderResponse>(),
                SellOrders = sellOrdersResponse ?? new List<SellOrderResponse>()
            };

            return View("Orders", orders);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            ViewBag.currentUrl = HttpContext.Request.Path;
            //Set the DateAndTimeOfOrder to the current date and time
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            //re-validate the model
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stock = new StockTrade()
                {
                    StockSymbol = sellOrderRequest.StockSymbol,
                    StockName = sellOrderRequest.StockName,
                    Quantity = sellOrderRequest.Quantity
                };

                return View("Index", stock);
            }
            SellOrderResponse sellOrderResponse =await _stocksService.CreateSellOrder(sellOrderRequest);
            List<BuyOrderResponse> buyOrdersResponse = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrdersResponse = await _stocksService.GetSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrdersResponse ?? new List<BuyOrderResponse>(),
                SellOrders = sellOrdersResponse ?? new List<SellOrderResponse>()
            };
           


            return View("Orders", orders);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            ViewBag.currentUrl = HttpContext.Request.Path;
            ViewBag.ActionName = ControllerContext.ActionDescriptor.ActionName;

            List<BuyOrderResponse> buyOrdersResponse = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrdersResponse = await _stocksService.GetSellOrders();

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
            List<IOrderResponsePdf> orders = new List<IOrderResponsePdf>();
            orders.AddRange(await _stocksService.GetBuyOrders());
            orders.AddRange(await _stocksService.GetSellOrders());

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

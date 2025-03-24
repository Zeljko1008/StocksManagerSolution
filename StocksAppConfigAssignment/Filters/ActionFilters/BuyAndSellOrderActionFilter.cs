using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StocksAppConfigAssignment.Controllers;
using StocksAppConfigAssignment.Models;

namespace StocksAppConfigAssignment.Filters.ActionFilters
{
    public class BuyAndSellOrderActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<BuyAndSellOrderActionFilter> _logger;

        public BuyAndSellOrderActionFilter(ILogger<BuyAndSellOrderActionFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Executing BuyAndSellOrderActionFilter");
            //TO DO: before logic

            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;
                _logger.LogInformation("ActionArguments:");

                foreach (var arg in context.ActionArguments)
                {
                    _logger.LogInformation("Key: {Key}, Value: {Value}", arg.Key, arg.Value);
                }


                //logg values of orderRequest
                _logger.LogInformation("StockSymbol: {StockSymbol}, StockName: {StockName}, Quantity: {Quantity}, Price: {Price}, DateAndTimeOfOrder: {DateAndTimeOfOrder}", orderRequest?.StockSymbol, orderRequest?.StockName, orderRequest?.Quantity, orderRequest?.Price, orderRequest?.DateAndTimeOfOrder);

                if (orderRequest != null)
                {

                    //update date of order
                    orderRequest.DateAndTimeOfOrder = DateTime.Now;

                    //re-validate the model object after updating the date
                    tradeController.ModelState.Clear();
                    tradeController.TryValidateModel(orderRequest);

                    //logg orderRequest values after updating the date
                    _logger.LogInformation("StockSymbol: {StockSymbol}, StockName: {StockName}, Quantity: {Quantity}, Price: {Price}, DateAndTimeOfOrder: {DateAndTimeOfOrder}", orderRequest?.StockSymbol, orderRequest?.StockName, orderRequest?.Quantity, orderRequest?.Price, orderRequest?.DateAndTimeOfOrder);




                    if (!tradeController.ModelState.IsValid)
                    {
                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();



                        StockTrade stockTrade = new StockTrade() { StockName = orderRequest.StockName, Quantity = orderRequest.Quantity, StockSymbol = orderRequest.StockSymbol };

                        context.Result = tradeController.View(nameof(TradeController.Index), stockTrade); //short-circuits or skips the subsequent action filters & action method
                    }

                    else
                    {
                        await next(); //invokes the subsequent filter or action method
                    }
                }
                else
                {
                    await next(); //invokes the subsequent filter or action method
                }
            }
            else
            {
                await next(); //calls the subsequent filter or action method
            }

            //TO DO: before logic
        }
    }
}

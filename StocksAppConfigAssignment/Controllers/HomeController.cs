using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StocksAppConfigAssignment.Controllers
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(exceptionHandlerPathFeature !=null && exceptionHandlerPathFeature.Error != null)
            {
                ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            }
            return View();
        }
    }
}

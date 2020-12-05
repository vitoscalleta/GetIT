using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace GetIT.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            //var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionMessage = statusCode.ToString();// exceptionDetails.Error.Message;
            logger.LogError($"Error occurred. Path: {statusCode}");
            return View();
        }


        [Route("/Error")]
        public IActionResult ErrorAction()
        {
            //var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Path = exceptionDetails.Path;
            ViewBag.Exception = exceptionDetails.Error;
            return View();
        }
    }
}
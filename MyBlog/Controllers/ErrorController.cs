using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;

namespace MyBlog.Controllers
{
    public class ErrorController : Controller
    {

        private readonly Logger _logger;

        public ErrorController()
        {
            _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // Your exception
            _logger.Error(exception);            
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Page404")]
        public IActionResult Page404()
        {
            string? originalPath = "unknown";
            if (HttpContext.Items.TryGetValue("originalPath", out object? value))
            {
                originalPath = value as string;
            }
            _logger.Error("Был переход по несуществующей ссылке: {0}", originalPath);
            return View();
        }
    }
}

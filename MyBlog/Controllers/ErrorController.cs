using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("Error")]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [Route("Page404")]
        public ActionResult Page404()
        {
            return View();
        }
    }
}

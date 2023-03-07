using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorController : Controller
    {

        [Route("Error")]
        public ActionResult Error()
        {
            return View();
        }


        [Route("404")]
        public ActionResult Http404()
        {
            return View();
        }
    }
}

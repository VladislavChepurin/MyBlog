using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers.Articles
{
    public class TegController : Controller
    {
        // GET: TegController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TegController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TegController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TegController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TegController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TegController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TegController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TegController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

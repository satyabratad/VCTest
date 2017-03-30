using System;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    /// <summary>
    /// Home Controller is used for namvigation to Inde, About Us and Conact Us page
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Redirects to Index page
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            var year = DateTime.Now.Year - 1;
            return RedirectToAction("Index","IRSProcess", new { id = year });
        }

        /// <summary>
        /// Redirects to About Us page
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Redirects to Contact Us
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}
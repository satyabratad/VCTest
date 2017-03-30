using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    /// <summary>
    /// Displays generic error page
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        ///  GET: Error
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  GET: Page Not Found
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult NotFound()
        {
            return View();
        }

        /// <summary>
        ///  GET: Un Authorized Access
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
    }
}
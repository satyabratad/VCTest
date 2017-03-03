using Bill2Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    public class ReportController : Controller
    {
        ApplicationDbContext dbContext = null;

        public ReportController()
        {
            dbContext = new ApplicationDbContext();
        }

        public ActionResult Tin()
        {
            var items = dbContext.ImportDetails.Where(p => p.IsActive == true);

            return View(items);
        }

        public ActionResult K1099()
        {
            return View();
        }

        public ActionResult Merchant()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Tin()
        {
            return View();
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
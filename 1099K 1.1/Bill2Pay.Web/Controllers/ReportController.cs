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
            var items = dbContext.SubmissionDetails.Where(p => p.IsActive == true);

            return View(items);
        }

        public ActionResult Merchant(int? Id,int? payer)
        {
            if(Id == null)
            {
                Id = DateTime.Now.Year - 1;

                return RedirectToAction("Merchant", new { id = Id });
            }
            if(payer == null)
            {
                payer = 0;
            }
            var items = dbContext.MerchantDetails
                .Include("Payer")
                .Where(p => p.IsActive == true && p.PaymentYear == Id && ((payer == 0) || (p.PayerId == payer)));

            return View(items);
        }
    }
}
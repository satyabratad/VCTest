using Bill2Pay.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    /// <summary>
    /// Report Controler
    /// Helpt to Generate TIN, K1099, Merchant Details Report
    /// </summary>
    public class ReportController : Controller
    {
        ApplicationDbContext dbContext = null;

        /// <summary>
        /// Default Contructor
        /// </summary>
        public ReportController()
        {
            dbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// Generate TIN Check Status Report
        /// </summary>
        /// <param name="Id">Year</param>
        /// <param name="payer">Payer Id as Integer</param>
        /// <returns>ActionResult as ImportDetails</returns>
        public ActionResult Tin(int? Id, int? payer)
        {
            if (Id == null)
            {
                Id = DateTime.Now.Year - 1;

                return RedirectToAction("Tin", new { id = Id });
            }
            if (payer == null)
            {
                payer = 0;
            }

            var items = dbContext.ImportDetails.Where(p => p.IsActive == true && p.ImportSummary.PaymentYear == Id && ((payer == 0) || (p.Merchant.PayerId == payer)));

            return View(items);
        }

        /// <summary>
        /// Generate 1099-K submission Report
        /// </summary>
        /// <param name="Id">Year</param>
        /// <param name="payer">Payer Id as Integer</param>
        /// <returns>ActionResult</returns>
        public ActionResult K1099(int? Id, int? payer)
        {
            if (Id == null)
            {
                Id = DateTime.Now.Year - 1;

                return RedirectToAction("K1099", new { id = Id });
            }
            if (payer == null)
            {
                payer = 0;
            }

            var items = dbContext.ImportDetails.Where(p => p.IsActive == true && p.ImportSummary.PaymentYear == Id && ((payer == 0) || (p.Merchant.PayerId == payer))).ToList();

            return View(items);
        }

        /// <summary>
        /// Generates Merchant Management Report
        /// </summary>
        /// <param name="Id">Year</param>
        /// <param name="payer">Payer Id as Integer</param>
        /// <returns>ActionResult as Merchant</returns>
        public ActionResult Merchant(int? Id,int? payer)
        {
            
            if(payer == null)
            {
                payer = 0;
            }
            var items = dbContext.MerchantDetails
                .Include("Payer")
                .Where(p => p.IsActive == true && ((payer == 0) || (p.PayerId == payer)));

            return View(items);
        }
    }
}
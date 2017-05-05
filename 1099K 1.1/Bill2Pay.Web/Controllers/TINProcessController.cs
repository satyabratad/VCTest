using Bill2Pay.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    /// <summary>
    /// Generate Input file for TIN Check
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class TINProcessController : Controller
    {
        ApplicationDbContext dbContext = null;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TINProcessController()
        {
            dbContext = new ApplicationDbContext();
        }
        
        /// <summary>
        /// Action Method Index
        /// Returns the View as Action Results.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Generates TIN maching input file
        /// and downloads to user client machine
        /// </summary>
        /// <returns>FileContentResult</returns>
        public FileContentResult  TINMatchingInput()
        {
            string strFileline = string.Empty;
            int year = DateTime.Now.Year - 1; 
            try
            {
                var Merchantlist = (List<string>)TempData.Peek("CheckedMerchantList");
                if (TempData["SelectedYear"] != null)
                {
                    year = (int)(TempData["SelectedYear"]);
                }


                if (Merchantlist != null)
                {


                    var lstTin = dbContext.ImportDetails
                                          .Include("ImportSummary")
                                          .Where(p => Merchantlist.Contains(p.AccountNumber) && p.ImportSummary.PaymentYear == year && p.IsActive == true);


                    foreach (var itm in lstTin)
                    {
                        var payeeName = string.Format("{0} {1}", itm.FirstPayeeName, itm.SecondPayeeName);
                        payeeName = Regex.Replace(payeeName, "[^0-9A-Za-z-& ]+", "");
                        if(payeeName.Length > 40)
                        {
                            payeeName = payeeName.Substring(0, 40);
                        }
                        strFileline = strFileline + itm.TINType + ";" + itm.TIN + ";" + payeeName.Trim() + ";" + itm.AccountNumber + Environment.NewLine;
                    }


                }
            }
            catch( IOException ex )
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            HttpCookie cookie = new HttpCookie("_FileDownloaded");
            cookie.Value = "DONE";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return File(new System.Text.UTF8Encoding().GetBytes(strFileline), "text/csv", "TinMatch.txt");
        }
    }
}
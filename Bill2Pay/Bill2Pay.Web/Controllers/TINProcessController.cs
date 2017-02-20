using Bill2Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TINProcessController : Controller
    {
        ApplicationDbContext dbContext = null;

        public TINProcessController()
        {
            dbContext = new ApplicationDbContext();
        }
        // GET: TIN
        public ActionResult Index()
        {
            return View();
        }

        public FileContentResult  TINMatchingInput()
        {
            string strFileline = string.Empty;
            int year = 2016;
            var  Merchantlist = (List<string>)TempData.Peek("CheckedMerchantList");
            if(TempData["SelectedYear"]!=null)
            {
                year = (int)(TempData["SelectedYear"]);
            }
            

            if (Merchantlist!=null)
            {
               

                var lstTin = dbContext.ImportDetails
                                      .Include("ImportSummary")
                                      .Where(p => Merchantlist.Contains(p.AccountNo) && p.ImportSummary.PaymentYear==year && p.IsActive==true);

                //strFileline = "TINTYPE;TINNUMBER;NAME;ACCOUNTNUMBER" + Environment.NewLine; 

                foreach(var itm in lstTin)
                {
                    strFileline= strFileline + itm.TINType +";"+ itm.TIN +";"+itm.FirstPayeeName+";"+itm.AccountNo + Environment.NewLine;
                }

               
            }
            return File(new System.Text.UTF8Encoding().GetBytes(strFileline), "text/csv", "TinMatch.txt");
        }

        
    }
}
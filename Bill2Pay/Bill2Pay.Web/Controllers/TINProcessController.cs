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
            var  Merchantlist = (List<string>)TempData.Peek("CheckedMerchantList");
            

            if (Merchantlist!=null)
            {
               

                var lstTin = dbContext.ImportDetails.Where(p => Merchantlist.Contains(p.AccountNo) && p.TINCheckStatus==null);

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
using Bill2Pay.Model;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bill2Pay.GenerateIRSFile;

namespace Bill2Pay.Web.Controllers
{
    public class IRSProcessController : Controller
    {
        ApplicationDbContext dbContext = null;

        public IRSProcessController()
        {
            dbContext = new ApplicationDbContext();
        }

        
        // GET: IRSProcess
        public ActionResult Index(int? ID)
        {
            
            var year = DateTime.Now.Year - 1;
            // ID - > Year
            if (ID!=null)
            {
                year =(int) ID;
            }

            var merchantlst = dbContext.ImportDetails
                                .Include("ImportSummary")
                                .Where(i=>i.ImportSummary.PaymentYear==year)                         
                                .ToList();

            var merchantAccList = dbContext.ImportDetails.Select(p =>
                                 new MerchantVM 
                                 {
                                     AccountNo = p.AccountNo,
                                     IsChecked = 0
                                 }).ToList();

            ViewBag.SelectedYear = year;
            ViewBag.lstmerchantAcc =  JsonConvert.SerializeObject(merchantAccList);

                return View(merchantlst);
        }

        public ActionResult Details()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Process(string btnPressed)
        {
            var chkList = Request.Form["checkedAccountNo"];

            List<MerchantVM> Merchatlist = new JavaScriptSerializer().Deserialize<List<MerchantVM>>(chkList);

            List<MerchantVM> checkedList = Merchatlist.Where(m => m.IsChecked == 1).ToList();
            var list= Merchatlist.Where(m => m.IsChecked == 1).Select(m=> m.AccountNo).ToList();
            TempData["CheckedMerchantList"] = list;

            if (!string.IsNullOrEmpty(Request.Form["tinmatching"]))
            {
                // Call tinmatching process
                return RedirectToAction("TINMatchingInput", "TINProcess");
            }
            else if (!string.IsNullOrEmpty(Request.Form["irstest"]))
            {
                return RedirectToAction("IRSFireTestFile");
            }
            else if (!string.IsNullOrEmpty(Request.Form["irs"]))
            {
                return RedirectToAction("IRS");
            }
            else if (!string.IsNullOrEmpty(Request.Form["irscorrection"]))
            {
                return RedirectToAction("IRScorrection");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult IRSFireTestFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            GenerateTaxFile taxFile = new GenerateTaxFile(true, 2016, 1, selectedMerchants);

            taxFile.ReadFromSchemaFile();
            return View();
        }

        public ActionResult IRS()
        {
            return View();
        }

        public ActionResult IRScorrection()
        {
            return View();
        }

        public ActionResult Download(string file)
        {
            file = @"C:\B2P\" + file;
            if (!System.IO.File.Exists(file))
            {
                return HttpNotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = "download.txt"
            };
            return response;
        }
    }
}
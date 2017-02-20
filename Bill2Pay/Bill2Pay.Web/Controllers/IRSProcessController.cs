﻿using Bill2Pay.Model;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bill2Pay.GenerateIRSFile;
using System.Web.Hosting;

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
            if (ID != null)
            {
                year = (int)ID;
            }

         

            var merchantlst = (dbContext.ImportDetails
                            .Include("ImportSummary")
                            .GroupJoin(dbContext.SubmissionStatus,
                            imp => imp.AccountNo,
                            stat => stat.AccountNumber,
                            (imp, stat) => new MerchantListVM() { ImportDetails = imp, SubmissionStatus = stat.FirstOrDefault() })
                            .Where(x => x.ImportDetails.ImportSummary.PaymentYear == year)
                            ).ToList();
 



            var merchantAccList = dbContext.ImportDetails.Select(p =>
                                 new MerchantVM
                                 {
                                     AccountNo = p.AccountNo,
                                     IsChecked = 0
                                 }).ToList();

            ViewBag.SelectedYear = year;
            ViewBag.lstmerchantAcc = JsonConvert.SerializeObject(merchantAccList);

            return View(merchantlst);
        }

        public ActionResult Details(string Id)
        {
            var data = ApplicationDbContext.Instence
                .SubmissionDetails
                .Include("PSE")
                .OrderByDescending(p=>p.SubmissionId )
                .FirstOrDefault(p => p.AccountNo.Equals(Id, StringComparison.OrdinalIgnoreCase));
            return View(data);
        }



        [HttpPost]
        public ActionResult Process(string btnPressed)
        {
            var chkList = Request.Form["checkedAccountNo"];

            List<MerchantVM> Merchatlist = new JavaScriptSerializer().Deserialize<List<MerchantVM>>(chkList);

            List<MerchantVM> checkedList = Merchatlist.Where(m => m.IsChecked == 1).ToList();
            var list = Merchatlist.Where(m => m.IsChecked == 1).Select(m => m.AccountNo).ToList();
            TempData["CheckedMerchantList"] = list;

            if (!string.IsNullOrEmpty(Request.Form["tinmatching"]))
            {
                // Call tinmatching process

                //TODO: limit can be read from config file
                if (checkedList.Count > 100000)
                {

                    return RedirectToAction("Index");
                }
                return RedirectToAction("TINMatchingInput", "TINProcess");
            }
            else if (!string.IsNullOrEmpty(Request.Form["irstest"]))
            {
                return RedirectToAction("IRSFireTestFile");
            }
            else if (!string.IsNullOrEmpty(Request.Form["irs"]))
            {
                return RedirectToAction("IRSFireFile");
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

            GenerateTaxFile taxFile = new GenerateTaxFile(true, 2016, 8, selectedMerchants);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSInputFile_Test.txt";
            return View();
        }

        public ActionResult IRSFireFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];

            string errorTINResult = "1,2,3,4,5";

            //TODO : Check if TIN Status is pass before creating the file.

            var tinCheckedPayeeList = ApplicationDbContext.Instence.ImportDetails
                .Where(x => selectedMerchants.Contains(x.AccountNo)).ToList();

            var incorrectTINresult = tinCheckedPayeeList.Where(x => x.TINCheckStatus == null || errorTINResult.Contains(x.TINCheckStatus)).ToList();

            if (incorrectTINresult.Count != 0)
                return RedirectToAction("Index", "Home");

            GenerateTaxFile taxFile = new GenerateTaxFile(false, 2016, 8, selectedMerchants);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSInputFile.txt";
            return View("IRSFireTestFile");
        }

        public ActionResult IRScorrection()
        {
            return View();
        }
        public ActionResult Download(string file)
        {
            string path = string.Format(@"{0}App_Data\Download\Irs\" + file, HostingEnvironment.ApplicationPhysicalPath);
            
            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(path);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = "download.txt"
            };
            return response;
        }
    }
}

using Bill2Pay.Model;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bill2Pay.GenerateIRSFile;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;

namespace Bill2Pay.Web.Controllers
{
    [Authorize]
    public class IRSProcessController : Controller
    {
        ApplicationDbContext dbContext = null;

        public IRSProcessController()
        {
            dbContext = new ApplicationDbContext();
        }


        // GET: IRSProcess
        public ActionResult Index(int? Id)
        {
            if(Id == null)
            {
                var year = DateTime.Now.Year - 1;
                return RedirectToAction("Index", "IRSProcess", new { id = year });
            }
            
            TempData["year"] = Id.ToString();


            List<MerchantListVM> merchantlst = (dbContext.ImportDetails
                            .Include("ImportSummary")
                            .GroupJoin(dbContext.SubmissionStatus.Where(s=>s.IsActive==true), //   .DefaultIfEmpty(),
                            imp => imp.AccountNo,
                            stat => stat.AccountNumber ,                          
                            (imp, stat) => new MerchantListVM() { ImportDetails = imp, SubmissionStatus = stat.FirstOrDefault() })
                            .Where(x => x.ImportDetails.ImportSummary.PaymentYear == Id && x.ImportDetails.IsActive==true && x.ImportDetails.TIN !=null)
                            ).OrderBy(x=>x.ImportDetails.AccountNo).ToList();

           
            var merchantAccList = merchantlst.Select(t =>
                                        new MerchantVM
                                        {
                                            AccountNo = t.ImportDetails.AccountNo,
                                            IsChecked = 0
                                        }).ToList(); 
                                    
            

            ViewBag.SelectedYear = Id;
            ViewBag.lstmerchantAcc = JsonConvert.SerializeObject(merchantAccList);
            ViewBag.ErrorMsg = string.Empty ;
            if (TempData["ErrorMsg"]!=null)
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
            }
            return View(merchantlst);
        }

        public ActionResult Details(string Id)
        {
            //var data = ApplicationDbContext.Instence
            //    .SubmissionDetails
            //    .Include("PSE")
            //    .OrderByDescending(p => p.SubmissionId)
            //    .FirstOrDefault(p => p.AccountNo.Equals(Id, StringComparison.OrdinalIgnoreCase) && p.IsActive == true);

            var data = ApplicationDbContext.Instence
                .ImportDetails
                .Include("Merchant")
                .OrderByDescending(p => p.ImportSummaryId)
                .FirstOrDefault(p => p.AccountNo.Equals(Id, StringComparison.OrdinalIgnoreCase) && p.IsActive == true);

            return View(data);
        }

        [HttpPost]
        public ActionResult Process(string btnPressed)
        {
            var chkList = Request.Form["checkedAccountNo"];
            var year =int.Parse( Request.Form["ddlYear"]);
            string statusId = Request.Form["statusId"];

            List<MerchantVM> Merchatlist = new JavaScriptSerializer().Deserialize<List<MerchantVM>>(chkList);

            List<MerchantVM> checkedList = Merchatlist.Where(m => m.IsChecked == 1).ToList();
            var list = Merchatlist.Where(m => m.IsChecked == 1).Select(m => m.AccountNo).ToList();

            TempData["CheckedMerchantList"] = list;
            TempData["SelectedYear"] = year;
            TempData["statusId"] = statusId;

            if (checkedList.Count == 0)
            {
                TempData["errorMessage"] = "Please select at least one merchant to perform this action.";
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(Request.Form["tinmatching"]))
            {
                //TODO: limit can be read from config file
                if (checkedList.Count > 100000)
                {
                    TempData["errorMessage"] = "Maximum 100000 merchant is allowed for TIN matching input.";
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
            else if(!string.IsNullOrEmpty(statusId))
            {
                return RedirectToAction("ChangeStatus");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult IRSFireTestFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            Int32 year = Convert.ToInt32(TempData["year"]);
            if (selectedMerchants.Count == 0)
            {
                TempData["errorMessage"] = "Select atleast one record to generate IRS Test File";
                return RedirectToAction("Index", "Home");
            }

            GenerateTaxFile taxFile = new GenerateTaxFile(true, year, User.Identity.GetUserId<long>(), selectedMerchants);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSInputFile_Test.txt";
            return View();
        }

        public ActionResult IRSFireFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            if (selectedMerchants.Count == 0)
            {
                TempData["errorMessage"] = "Select atleast one record to generate IRS File";
                return RedirectToAction("Index", "Home");
            }
            Int32 year = Convert.ToInt32(TempData["year"]);

            string errorTINResult = "1,2,3,4,5";

            var tinCheckedPayeeList = ApplicationDbContext.Instence.ImportDetails
                .Join(ApplicationDbContext.Instence.ImportSummary, d => d.ImportSummaryId, s => s.Id, (d, s) => new { detail = d, summary = s })
                .Where(x => selectedMerchants.Contains(x.detail.AccountNo) && x.summary.PaymentYear == year && x.detail.IsActive==true && x.summary.IsActive==true).ToList();

            var incorrectTINresult = tinCheckedPayeeList.Where(x => x.detail.TINCheckStatus == null || errorTINResult.Contains(x.detail.TINCheckStatus)).ToList();

            if (incorrectTINresult.Count != 0)
            {
                TempData["errorMessage"] = "At least one of the selected merchants has negative or void TIN check result. IRS FIRE Input file cannot be generated for this selection.";
                return RedirectToAction("Index", "Home");
            }

            //var alreadySubmitted = tinCheckedPayeeList.Where(x => x.detail.SubmissionSummaryId != null).ToList();
            string doNotSubmit = "2,3,5";
            var alreadySubmitted = ApplicationDbContext.Instence.ImportDetails
                .Join(ApplicationDbContext.Instence.SubmissionStatus, d => d.AccountNo, s => s.AccountNumber, (d, s) => new { details = d, status = s })
                .Where(x => selectedMerchants.Contains(x.details.AccountNo) && x.details.IsActive==true && x.status.IsActive==true &&
                doNotSubmit.Contains(x.status.StatusId.ToString())).ToList();

            if (alreadySubmitted.Count != 0)
            {
                TempData["errorMessage"] = "One or more than one selected merchant's 1009K file already generated. IRS file can not be generated for this selection";
                return RedirectToAction("Index", "Home");
            }

            GenerateTaxFile taxFile = new GenerateTaxFile(false, 2016, User.Identity.GetUserId<long>(), selectedMerchants);

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

        public ActionResult ChangeStatus()
        {

            Int32 year = Convert.ToInt32(TempData["year"]);
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            Int32 statusId = Convert.ToInt32(TempData["statusId"]);

            if (statusId < 1)
            {
                TempData["errorMessage"] = "Requested status is not specified. Please select a list a try again.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var item in selectedMerchants)
            {
                var previousData = dbContext.SubmissionStatus.Where(x => x.AccountNumber.Equals(item) && x.PaymentYear.Equals(year) && x.IsActive == true).ToList();

                if (previousData != null)
                {
                    foreach (var data in previousData)
                    {
                        data.IsActive = false;
                    }
                }
                var submissionStatus = new SubmissionStatus();

                submissionStatus.PaymentYear = year;
                submissionStatus.AccountNumber = item;
                submissionStatus.ProcessingDate = DateTime.Now;
                submissionStatus.StatusId = statusId;
                submissionStatus.DateAdded = DateTime.Now;
                submissionStatus.IsActive = true;

                dbContext.SubmissionStatus.Add(submissionStatus);
                dbContext.SaveChanges();
            }
            TempData["successMessage"] = "Submission status updated successfully.";

            return RedirectToAction("Index", "Home");
        }
    }
}

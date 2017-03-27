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



        public ActionResult Index(int? Id, int? payer)
        {

            List<MerchantListVM> merchantlst;
            if (payer == null)
            {
                payer = 0;
                if (TempData["SelectedPayer"] != null)
                {
                    Int32 selectedpayer = Convert.ToInt32(TempData["SelectedPayer"]);
                    payer = selectedpayer;
                }

            }

            if (Id == null)
            {
                var year = DateTime.Now.Year - 1;
                return RedirectToAction("Index", "IRSProcess", new { id = year, payer = payer });
            }

            TempData["year"] = Id.ToString();
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];

            merchantlst = (dbContext.ImportDetails
                               .Include("ImportSummary")
                               .GroupJoin(dbContext.SubmissionStatus.Where(s => s.IsActive == true && s.PaymentYear==Id),
                               imp => imp.AccountNumber,
                               stat => stat.AccountNumber,
                               (imp, stat) => new MerchantListVM() { ImportDetails = imp, SubmissionStatus = stat.FirstOrDefault() })
                               .Where(x => x.ImportDetails.ImportSummary.PaymentYear == Id && x.ImportDetails.IsActive == true && x.ImportDetails.TIN != null
                                       && (payer == 0 || x.ImportDetails.Merchant.PayerId == payer))
                               ).OrderBy(x => x.ImportDetails.AccountNumber).ToList();


            var merchantAccList = merchantlst.Select(t =>
                                        new MerchantVM
                                        {
                                            AccountNumber = t.ImportDetails.AccountNumber,
                                            IsChecked = 0
                                        }).ToList();
            if (selectedMerchants != null)
                merchantAccList.ForEach(i => i.IsChecked = (selectedMerchants.FirstOrDefault(x => x.Equals(i.AccountNumber))) == null ? 0 : 1);

            ViewBag.SelectedYear = Id;
            ViewBag.lstmerchantAcc = JsonConvert.SerializeObject(merchantAccList);
            ViewBag.ErrorMsg = string.Empty;
            if (TempData["ErrorMsg"] != null)
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
            }
            return View(merchantlst);
        }

        public ActionResult Details(string Id,int? year)
        {

            MerchantListVM detail = dbContext.ImportDetails 
                .Include("Merchant")
                .GroupJoin(dbContext.SubmissionStatus.Where(s => s.IsActive == true && s.PaymentYear== year),
                    imp=> imp.AccountNumber,
                    stat => stat.AccountNumber,
                    (imp, stat)=> new MerchantListVM() { ImportDetails = imp, SubmissionStatus = stat.FirstOrDefault() }) 
                .OrderByDescending(p => p.ImportDetails.ImportSummaryId)
                .FirstOrDefault(p => p.ImportDetails.AccountNumber.Equals(Id, StringComparison.OrdinalIgnoreCase) && p.ImportDetails.ImportSummary.PaymentYear == year && p.ImportDetails.IsActive == true);


            var data=(ImportDetail) detail.ImportDetails ;
            if (detail.SubmissionStatus == null || detail.SubmissionStatus.Status.Id <= 2)
                data.SubmissionSummaryId = null;

            var merchant = dbContext.MerchantDetails.Where(m => m.Id == data.MerchantId).FirstOrDefault();
            data.Merchant = merchant;
            ViewBag.SelectedYear = year;
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Process(string btnPressed)
        {
            var chkList = Request.Form["checkedAccountNo"];
            var year = int.Parse(Request.Form["ddlYear"]);
            var selectedPayer = int.Parse(Request.Form["ddlPayer"]);
            string statusId = Request.Form["statusId"];

            List<MerchantVM> Merchatlist = new JavaScriptSerializer().Deserialize<List<MerchantVM>>(chkList);

            List<MerchantVM> checkedList = Merchatlist.Where(m => m.IsChecked == 1).ToList();
            var list = Merchatlist.Where(m => m.IsChecked == 1).Select(m => m.AccountNumber).ToList();

            TempData["CheckedMerchantList"] = list;
            TempData["SelectedYear"] = year;
            TempData["statusId"] = statusId;
            TempData["SelectedPayer"] = selectedPayer;

            if (checkedList.Count == 0)
            {
                TempData["errorMessage"] = "Please select at least one merchant to perform this action.";
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(Request.Form["tinmatching"]))
            {

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
            else if (!string.IsNullOrEmpty(statusId))
            {
                return RedirectToAction("ChangeStatus");
            }
            else if(!string.IsNullOrEmpty(Request.Form["generatepdf"]))
            {
                return RedirectToAction("GenerateBatchpdf");
            }
            else
            {
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GenerateBatchpdf()
        {
            string errMsg = string.Empty;
            string strMsg = string.Empty;
            List<string>list=(List<string>)TempData.Peek("CheckedMerchantList");
            int year=(int)TempData.Peek("SelectedYear") ;
            int selectedPayer= (int)TempData.Peek("SelectedPayer") ;

            var substatusList = dbContext.SubmissionStatus.Where(s=>s.IsActive == true && (s.StatusId ==(int)RecordStatus.Submitted  ||s.StatusId==(int) RecordStatus.ReSubmitted)
                                                              && s.PaymentYear == year).Select(p=>p.AccountNumber).ToList();
            var printableList= list.Where(l => substatusList.Contains(l)).ToList();
            TempData["PrintableMerchantList"]= printableList;

            var exceptList = list.Where(l => !substatusList.Contains(l)).ToList();
            if (exceptList != null && exceptList.Count>0)
            {
                var invalideAccounts = exceptList.Aggregate((i, j) => i + ", " + j);
                errMsg = "Unable to Generate pdf for " + invalideAccounts + ".";
            }
            
            if (printableList.Count()>0)
            {
                strMsg = "Generate .pdf file process may take some time. Once completed you can find the files in the '/App_Data/Download/k1099' location. ";
                TempData["successMessage"] = strMsg;
                TempData["errorMessage"] = errMsg;
                return RedirectToAction("PrintAllCopies", "Print");
            }
            else
            {
                strMsg = string.Empty;
                TempData["successMessage"] = strMsg;
                TempData["errorMessage"] = errMsg;
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }
            
            
            
           
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IRSFireTestFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            Int32 year = Convert.ToInt32(TempData["SelectedYear"]);
            Int32 selectedPayer = Convert.ToInt32(TempData["SelectedPayer"]);
            if (selectedMerchants.Count == 0)
            {
                TempData["errorMessage"] = "Select atleast one record to generate IRS Test File";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }

            IRSFileUtility taxFile = new IRSFileUtility(true, year, User.Identity.GetUserId<long>(), selectedMerchants);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSInputFile_Test.txt";

            string path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile_Test.txt", HostingEnvironment.ApplicationPhysicalPath);

            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound();
            }

            return File(path, "text", "IRSInputFile_Test.txt");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IRSFireFile()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];

            Int32 year = Convert.ToInt32(TempData["SelectedYear"]);
            Int32 selectedPayer = Convert.ToInt32(TempData["SelectedPayer"]);


            string errorTINResult = "1,2,3,4,5";

            var tinCheckedPayeeList = ApplicationDbContext.Instence.ImportDetails
                .Join(ApplicationDbContext.Instence.ImportSummary, d => d.ImportSummaryId, s => s.Id, (d, s) => new { detail = d, summary = s })
                .Where(x => selectedMerchants.Contains(x.detail.AccountNumber) && x.summary.PaymentYear == year && x.detail.IsActive == true && x.summary.IsActive == true).ToList();

            var incorrectTINresult = tinCheckedPayeeList.Where(x => x.detail.TINCheckStatus == null || errorTINResult.Contains(x.detail.TINCheckStatus)).ToList();

            if (incorrectTINresult.Count != 0)
            {
                TempData["errorMessage"] = "At least one of the selected merchant has negative or void TIN check result. IRS FIRE Input file cannot be generated for this selection.";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }

            //var alreadySubmitted = tinCheckedPayeeList.Where(x => x.detail.SubmissionSummaryId != null).ToList();
            string doNotSubmit = "3,5,6";
            var alreadySubmitted = ApplicationDbContext.Instence.ImportDetails
                .Include("ImportSummary")
                .Join(ApplicationDbContext.Instence.SubmissionStatus, d => d.AccountNumber, s => s.AccountNumber, (d, s) => new { details = d, status = s })
                .Where(x => selectedMerchants.Contains(x.details.AccountNumber) && x.details.IsActive == true && x.status.IsActive == true &&
                doNotSubmit.Contains(x.status.StatusId.ToString()) && x.details.ImportSummary.PaymentYear== year && x.status.PaymentYear==year).ToList();

            if (alreadySubmitted.Count != 0)
            {
                TempData["errorMessage"] = "One or more than one selected merchant's 1009K file already submitted. IRS file can not be generated for this selection";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }

            IRSFileUtility taxFile = new IRSFileUtility(false, year, User.Identity.GetUserId<long>(), selectedMerchants);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSInputFile.txt";

            string path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile.txt", HostingEnvironment.ApplicationPhysicalPath);

            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound();
            }

            return File(path, "text", "IRSInputFile.txt");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IRScorrection()
        {
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];

            Int32 year = Convert.ToInt32(TempData["SelectedYear"]);
            Int32 selectedPayer = Convert.ToInt32(TempData["SelectedPayer"]);

            string errorTINResult = "1,2,3,4,5";

            var tinCheckedPayeeList = ApplicationDbContext.Instence.ImportDetails
                .Join(ApplicationDbContext.Instence.ImportSummary, d => d.ImportSummaryId, s => s.Id, (d, s) => new { detail = d, summary = s })
                .Where(x => selectedMerchants.Contains(x.detail.AccountNumber) && x.summary.PaymentYear == year && x.detail.IsActive == true && x.summary.IsActive == true).ToList();

            var incorrectTINresult = tinCheckedPayeeList.Where(x => x.detail.TINCheckStatus == null || errorTINResult.Contains(x.detail.TINCheckStatus)).ToList();

            if (incorrectTINresult.Count != 0)
            {
                TempData["errorMessage"] = "At least one of the selected merchant has negative or void TIN check result. IRS FIRE Correction Input file cannot be generated for this selection.";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }

            //var alreadySubmitted = tinCheckedPayeeList.Where(x => x.detail.SubmissionSummaryId != null).ToList();
            string doNotSubmit = "1,2,3,5,6,7";
            var alreadySubmitted = ApplicationDbContext.Instence.ImportDetails
                .Include("ImportSummary")
                .Join(ApplicationDbContext.Instence.SubmissionStatus, d => d.AccountNumber, s => s.AccountNumber, (d, s) => new { details = d, status = s })
                .Where(x => selectedMerchants.Contains(x.details.AccountNumber) && x.details.IsActive == true && x.status.IsActive == true &&
                doNotSubmit.Contains(x.status.StatusId.ToString()) && x.details.ImportSummary.PaymentYear==year && x.status.PaymentYear==year).ToList();

            if (alreadySubmitted.Count != 0)
            {
                TempData["errorMessage"] = "One or more than one selected merchant's 1009K file already submitted. IRS Correction file can not be generated for this selection";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }

            IRSFileUtility taxFile = new IRSFileUtility(false, year, User.Identity.GetUserId<long>(), selectedMerchants, correction:true);

            taxFile.ReadFromSchemaFile();
            ViewBag.fileName = "IRSCorrectionInputFile.txt";

            string path = string.Format(@"{0}App_Data\Download\Irs\IRSCorrectionInputFile.txt", HostingEnvironment.ApplicationPhysicalPath);

            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound();
            }

            return File(path, "text", "IRSCorrectionInputFile.txt");
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeStatus()
        {

            Int32 year = Convert.ToInt32(TempData["year"]);
            Int32 selectedPayer = Convert.ToInt32(TempData["SelectedPayer"]);
            List<string> selectedMerchants = (List<string>)TempData["CheckedMerchantList"];
            Int32 statusId = Convert.ToInt32(TempData["statusId"]);

            if (statusId < 1)
            {
                TempData["errorMessage"] = "Requested status is not specified. Please select a list a try again.";
                return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
            }
            //changeble status;
            //if Not Submitted(1)=> status can not be modified.
            //if File Generated(2)=> status can be changed to => Submitted(6) Only.
            //if Correction Required(3) => status can not be changed.
            //if CorrectionUploaded(4) => status can not be changed.
            //if ReSubmitted(5)=> 

            foreach (var item in selectedMerchants)
            {
                var previousData = dbContext.SubmissionStatus.Where(x => x.AccountNumber.Equals(item) && x.PaymentYear.Equals(year) && x.IsActive == true).ToList();

                if (previousData.Count > 0)
                {
                    foreach (var data in previousData)
                    {
                        if (statusId == (int)RecordStatus.Submitted && (data.StatusId != (int)RecordStatus.FileGenerated && data.StatusId != (int)RecordStatus.OneTransactionCorrection && data.StatusId != (int)RecordStatus.TwoTransactionCorrection))
                        {
                            TempData["errorMessage"] = "Specified status can not be updated for : " + data.AccountNumber;
                            return RedirectToAction("Index", "Home");
                        }
                        else if (statusId == (int)RecordStatus.OneTransactionCorrection && data.StatusId != (int)RecordStatus.Submitted && data.StatusId != (int)RecordStatus.ReSubmitted)
                        {
                            return DisplayStatusChangeError(data.AccountNumber);
                        }
                        else if (statusId == (int)RecordStatus.TwoTransactionCorrection && data.StatusId != (int)RecordStatus.Submitted && data.StatusId!= (int)RecordStatus.ReSubmitted)
                        {
                            return DisplayStatusChangeError(data.AccountNumber);
                        }
                        else if (statusId == (int)RecordStatus.NotSubmitted && data.StatusId != (int)RecordStatus.Submitted && data.StatusId!= (int)RecordStatus.ReSubmitted)
                        {
                            return DisplayStatusChangeError(data.AccountNumber);
                        }

                        data.IsActive = false;
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Specified status can not be updated for : " + item;
                    return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", new { Id = year, payer = selectedPayer });
        }

        private ActionResult DisplayStatusChangeError(string accountNumber)
        {
            TempData["errorMessage"] = "Selected status can not be updated for : " + accountNumber;
            return RedirectToAction("Index", "Home");
        }
    }
}

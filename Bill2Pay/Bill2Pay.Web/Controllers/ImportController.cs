using Bill2Pay.GenerateIRSFile;
using Bill2Pay.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImportController : Controller
    {
        ImportUtility utility = null;
        ApplicationDbContext dbContext = null;

        public ImportController()
        {
            utility = new ImportUtility();
            dbContext = new ApplicationDbContext();
        }

        // GET: Import
        public ActionResult Index()
        {
            var year = DateTime.Now.Year - 1;
            return RedirectToAction("Transaction", new { id = year });
        }

        public ActionResult Transaction(int? Id, bool? Status)
        {
            if (Id == null)
            {
                var year = DateTime.Now.Year - 1;
                return RedirectToAction("Transaction", new { id = year });
            }


            var importSummary = ApplicationDbContext.Instence.ImportSummary
                .OrderByDescending(p => p.ImportDate).FirstOrDefault();
            if (importSummary == null)
            {
                importSummary = new ImportSummary();
                importSummary.ProcessLog = "";
            }
            if (Status == true)
            {
                ViewBag.SuccessMessage = "This will initiate a background process to import the transactions into the database. Once completed a transaction log will be generated.";
            }

            return View(importSummary);
        }

        [HttpPost]
        public async Task<ActionResult> Transaction(int? Id,int ddlPayer, HttpPostedFileBase fileBase)
        {
            if (Id == null || Id < 2015)
            {
                return View();
            }
            int year = (int)Id;
            string[] whiteListing = new string[] { ".zip", ".ZIP" };

            // Verify that the user selected a file
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                // Validation
                var extention = Path.GetExtension(fileBase.FileName);

                if (!whiteListing.Contains(extention))
                {
                    ViewBag.ValidationMessage = "Unsupported file format.";

                    var importSummary = ApplicationDbContext.Instence.ImportSummary
                            .OrderByDescending(p => p.ImportDate).FirstOrDefault();
                    if (importSummary == null)
                    {
                        importSummary = new ImportSummary();
                        importSummary.ProcessLog = "";
                    }

                    return View(importSummary);
                }


                // extract only the filename
                var fileName = Path.GetFileName(fileBase.FileName);

                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Transactions"), fileName);
                fileBase.SaveAs(path);

                utility.ProcessInputFileAsync(year, path, User.Identity.GetUserId<long>(), ddlPayer);


                return RedirectToAction("Transaction", new { Id = Id, Status = true });
            }

            return View();
        }



        public ActionResult Tin(int? Id)
        {

            if (Id == null)
            {
                var year = DateTime.Now.Year - 1;
                return RedirectToAction("Tin", new { id = year });
            }

            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Tin(int? Id, int? ddlPayer, HttpPostedFileBase fileBase)
        {
            DataTable dtTin = null;
            
            string result = string.Empty;
            //if (ddlPayer != null)
            //{
            //    payer = Convert.ToInt32(ddlPayer);
            //}

            if (Id == null || Id < 2015)
            {
                return View();
            }
            int year = (int)Id;
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                var extention = Path.GetExtension(fileBase.FileName);

                if (extention != ".txt")
                {
                    ViewBag.ValidationMessage = "Unsupported file format.";
                    return View();
                }


                // extract only the filename
                var fileName = Path.GetFileName(fileBase.FileName);

                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Tin"), fileName);
                fileBase.SaveAs(path);
                dtTin = ReadTinInput(path);
                result = UpdateTinMatchingStatus(dtTin, year, ddlPayer);
                ViewBag.Message = result;
            }
            return View();
        }


        private DataTable ReadTinInput(string fileName)
        {
            DataTable dtTin = new DataTable(); ;

            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataRow row;
            bool isFirstLine = true;

            char splitchar = ';';
            StreamReader sr = new StreamReader(fileName);

            while ((line = sr.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    strArray = line.Split(splitchar);
                    Array.ForEach(strArray, s => dtTin.Columns.Add(new DataColumn()));
                    isFirstLine = false;
                }
                row = dtTin.NewRow();
                row.ItemArray = line.Split(splitchar);
                dtTin.Rows.Add(row);
            }
            sr.Dispose();


            return dtTin;

        }

        private string UpdateTinMatchingStatus(DataTable dtTin, int year,int? payer)
        {
            string result = string.Empty;
            string accNo = string.Empty;
            string accName = string.Empty;
            string tin = string.Empty;
            string tinStaus = string.Empty;

            try
            {
                var imps = dbContext.ImportSummary.Where(s => s.PaymentYear == year).OrderByDescending(s => s.Id).FirstOrDefault();
                ImportDetail impd = null;
                foreach (DataRow dr in dtTin.Rows)
                {
                    tin = dr[1].ToString();
                    accName = dr[2].ToString();
                    accNo = dr[3].ToString();
                    tinStaus = dr[4].ToString();

                    var tinStatusName = dbContext.TINStatus.Where(t => t.Id.ToString() == tinStaus).FirstOrDefault();

                    if (payer == 0)
                    {
                        //ImportDetail impd1 = dbContext.ImportDetails.Where(i => i.TIN == tin && i.AccountNo == accNo && i.ImportSummary.Id == imps.Id).FirstOrDefault();
                         impd = dbContext.ImportDetails.Where(i => i.TIN.Equals(tin, StringComparison.InvariantCultureIgnoreCase)
                                                                         && i.FirstPayeeName.Equals(accName, StringComparison.InvariantCultureIgnoreCase)
                                                                         && i.ImportSummary.Id == imps.Id
                                                                         && i.IsActive == true
                                                                         ).FirstOrDefault();

                    }
                    else
                    {

                         impd = dbContext.ImportDetails.Where(i => i.TIN.Equals(tin, StringComparison.InvariantCultureIgnoreCase)
                                                                         && i.FirstPayeeName.Equals(accName, StringComparison.InvariantCultureIgnoreCase)
                                                                         && i.ImportSummary.Id == imps.Id
                                                                         && i.Merchant.Payer.Id == payer && i.IsActive==true
                                                                         ).FirstOrDefault();
                    }
                    ImportDetail newimpd = impd;
                    //ImportDetail newimpd1 = new ImportDetail()
                    //{
                    //    AccountNo = impd.AccountNo,
                    //    ImportSummaryId = impd.ImportSummaryId,
                    //    TINCheckStatus = impd.TINCheckStatus,
                    //    TINCheckRemarks = impd.TINCheckRemarks,
                    //    SubmissionSummaryId = impd.SubmissionSummaryId,
                    //    TINType = impd.TINType,
                    //    TIN = impd.TIN,
                    //    PayerOfficeCode = impd.PayerOfficeCode,
                    //    GrossAmount = impd.GrossAmount,
                    //    CNPTransactionAmount = impd.CNPTransactionAmount,
                    //    FederalWithHoldingAmount = impd.FederalWithHoldingAmount,
                    //    JanuaryAmount = impd.JanuaryAmount,
                    //    FebruaryAmount = impd.FebruaryAmount,
                    //    MarchAmount = impd.MarchAmount,
                    //    AprilAmount = impd.AprilAmount,
                    //    MayAmount = impd.MayAmount,
                    //    JuneAmount = impd.JuneAmount,
                    //    JulyAmount = impd.JulyAmount,
                    //    AugustAmount = impd.AugustAmount,
                    //    SeptemberAmount = impd.SeptemberAmount,
                    //    OctoberAmount = impd.OctoberAmount,
                    //    NovemberAmount = impd.NovemberAmount,
                    //    DecemberAmount = impd.DecemberAmount,
                    //    ForeignCountryIndicator = impd.ForeignCountryIndicator,
                    //    FirstPayeeName = impd.FirstPayeeName,
                    //    SecondPayeeName = impd.SecondPayeeName,
                    //    PayeeMailingAddress = impd.PayeeMailingAddress,
                    //    PayeeCity = impd.PayeeCity,
                    //    PayeeState = impd.PayeeState,
                    //    PayeeZipCode = impd.PayeeZipCode,
                    //    SecondTINNoticed = impd.SecondTINNoticed,
                    //    FillerIndicatorType = impd.FillerIndicatorType,
                    //    PaymentIndicatorType = impd.PaymentIndicatorType,
                    //    TransactionCount = impd.TransactionCount,
                    //    MerchantCategoryCode = impd.MerchantCategoryCode,
                    //    SpecialDataEntry = impd.SpecialDataEntry,
                    //    StateWithHolding = impd.StateWithHolding,
                    //    LocalWithHolding = impd.LocalWithHolding,
                    //    CFSF = impd.CFSF
                    //};


                    impd.IsActive = false;
                    dbContext.Entry(impd).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();

                    newimpd.TINCheckStatus = tinStaus;
                    newimpd.TINCheckRemarks = tinStatusName.Name;
                    newimpd.IsActive = true;
                    newimpd.DateAdded = DateTime.Now;

                    dbContext.ImportDetails.Add(newimpd);
                    dbContext.SaveChanges();
                }
                result = "TIN matching updated successfully";
            }
            catch (Exception ex)
            {
                result = "TIN matching updation failed";
            }


            return result;

        }
        public ActionResult Irs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Irs(HttpPostedFileBase fileBase)
        {
            return View();
        }


    }
}
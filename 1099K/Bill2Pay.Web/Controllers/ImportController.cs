using Bill2Pay.ExceptionLogger;
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
using System.Transactions;
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



        public ActionResult Tin(int? Id, int? payer)
        {
            //sint year = 0;
            if (Id == null)
            {
                Id = DateTime.Now.Year - 1;
                return RedirectToAction("Tin", new { id = Id, payer=payer });
            }
            if(payer== null)
            {
                payer = 0;
            }

            ViewBag.Message = "";
            return View();
            //return RedirectToAction("Tin", new { id = Id, payer = payer });
        }

        [HttpPost]
        public ActionResult Tin(int? Id, int? ddlPayer, HttpPostedFileBase fileBase)
        {
            DataTable dtTin = null;
            bool isSuccess = false;
            
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
                result = UpdateTinMatchingStatus(dtTin, year, ddlPayer, ref isSuccess);
                ViewBag.isSuccess = isSuccess;
                ViewBag.Message = result;
            }
            return View("Tin", new { id = year, payer= ddlPayer });
        }


        private DataTable ReadTinInput(string fileName)
        {
            DataTable dtTin = new DataTable(); 

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

        private string UpdateTinMatchingStatus(DataTable dtTin, int year, int? payer, ref bool isSuccess)
        {
            string result = string.Empty;
            string accNo = string.Empty;
            string accName = string.Empty;
            string tin = string.Empty;
            string tinStaus = string.Empty;

            using (var scope = new TransactionScope())
            {

                try
                {
                    // var imps = dbContext.ImportSummary.Where(s => s.PaymentYear == year && s.IsActive==true).OrderByDescending(s => s.Id).FirstOrDefault();

                    ImportDetail impd = null;
                    foreach (DataRow dr in dtTin.Rows)
                    {
                        if (dr.ItemArray.Length < 5)
                        {
                            result = "TIN matching updation failed as one or more records does not have required no. of column.";
                            isSuccess = false;
                            return result;
                        }
                        tin = dr[1].ToString();
                        accName = dr[2].ToString();
                        accNo = dr[3].ToString();
                        tinStaus = dr[4].ToString();

                        var tinStatusName = dbContext.TINStatus.Where(t => t.Id.ToString() == tinStaus).FirstOrDefault();

                        if (payer == 0)
                        {

                            impd = dbContext.ImportDetails
                                .Include("ImportSummary")
                                .Where(i => i.AccountNo.Equals(accNo, StringComparison.InvariantCultureIgnoreCase)
                                                            && i.ImportSummary.Id == i.ImportSummaryId
                                                            && i.ImportSummary.PaymentYear == year
                                                            && i.IsActive == true).FirstOrDefault();

                        }
                        else
                        {

                            impd = dbContext.ImportDetails
                                .Include("ImportSummary")
                                .Where(i => i.AccountNo.Equals(accNo, StringComparison.InvariantCultureIgnoreCase)
                                                           && i.ImportSummary.Id == i.ImportSummaryId
                                                           && i.ImportSummary.PaymentYear == year
                                                           && i.Merchant.Payer.Id == payer && i.IsActive == true).FirstOrDefault();
                        }
                        if (impd != null)
                        {
                            ImportDetail newimpd = impd;


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
                    }
                    scope.Complete();
                   
                    result = "TIN matching updated successfully";
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    result = "TIN matching updation failed";
                    isSuccess = false;
                    Logger.LogInstance.LogInfo("TIN matching updation failed:{0}", ex.StackTrace.ToString());
                    throw ex;
                }
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
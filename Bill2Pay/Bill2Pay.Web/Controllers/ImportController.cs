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

        public ActionResult Transaction()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Transaction(int Id, HttpPostedFileBase fileBase)
        {
            if(Id < 2015)
            {
                return View();
            }

            string[] whiteListing = new string[] { ".zip", ".ZIP" };

            // Verify that the user selected a file
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                // Validation
                var extention = Path.GetExtension(fileBase.FileName);

                if (!whiteListing.Contains(extention))
                {
                    ViewBag.ValidationMessage = "Unsupported file format.";
                    return View();
                }


                // extract only the filename
                var fileName = Path.GetFileName(fileBase.FileName);

                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Transactions"), fileName);
                fileBase.SaveAs(path);

                utility.ProcessInputFileAsync(Id, path, User.Identity.GetUserId<long>());

                return RedirectToAction("Transaction");
            }

            return View();
        }

        private List<SelectListItem> GetYearList()
        {
            List<SelectListItem> lstyear = new List<SelectListItem>();
            lstyear.Add(new SelectListItem { Value = "2016", Text = "2016" });
            lstyear.Add(new SelectListItem { Value = "2017", Text = "2017" });
            lstyear.Add(new SelectListItem { Value = "2018", Text = "2018" });
            lstyear.Add(new SelectListItem { Value = "2019", Text = "2019" });
            lstyear.Add(new SelectListItem { Value = "2020", Text = "2020" });
            lstyear.Add(new SelectListItem { Value = "2021", Text = "2021" });
            lstyear.Add(new SelectListItem { Value = "2022", Text = "2022" });
            lstyear.Add(new SelectListItem { Value = "2023", Text = "2023" });

            return lstyear;
        }

        
        public ActionResult Tin()
        {

            ViewBag.Message = "";
            //ViewBag.Yearlist = GetYearList();
            return View();
        }

        [HttpPost]
        public ActionResult Tin(int Id, HttpPostedFileBase fileBase)
        {
            DataTable dtTin = null;
            string result = string.Empty;
            //int year =int.Parse(Request["ddlyear"]);
            if (Id < 2015)
            {
                return View();
            }
            int year = Id;
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                // Validation
                var extention = Path.GetExtension(fileBase.FileName);

                if (extention !=".txt")
                {
                    ViewBag.ValidationMessage = "Unsupported file format.";
                    return View();
                }


                // extract only the filename
                var fileName = Path.GetFileName(fileBase.FileName);

                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Tin"), fileName);
                fileBase.SaveAs(path);
                dtTin=ReadTinInput(path);
                result = UpdateTinMatchingStatus(dtTin, year);
                ViewBag.Message = result;
                //ViewBag.Yearlist = GetYearList();
            }
            return View();
        }


        private DataTable ReadTinInput(string fileName)
        {
            DataTable dtTin= new DataTable(); ;

            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataRow row;
            bool isFirstLine = true;

            char splitchar = ';';
            //Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            StreamReader sr = new StreamReader(fileName);

            //line = sr.ReadLine();
            //strArray = line.Split(splitchar);// r.Split(line);
            //Array.ForEach(strArray, s => dtTin.Columns.Add(new DataColumn()));

            while ((line = sr.ReadLine()) != null)
            {
                if(isFirstLine)
                {
                    strArray = line.Split(splitchar);// r.Split(line);
                    Array.ForEach(strArray, s => dtTin.Columns.Add(new DataColumn()));
                    isFirstLine = false;
                }
                row = dtTin.NewRow();
                row.ItemArray = line.Split(splitchar);//  r.Split(line);
                dtTin.Rows.Add(row);
            }
            sr.Dispose();

           
            return dtTin;

        }

        private string UpdateTinMatchingStatus(DataTable dtTin,int year)
        {
            string result = string.Empty;
            string accNo = string.Empty;
            string tin = string.Empty;
            string tinStaus = string.Empty;

            try
            {
                var imps = dbContext.ImportSummary.Where(s => s.PaymentYear == year).OrderByDescending(s=>s.Id).FirstOrDefault();
                
                foreach (DataRow dr in dtTin.Rows)
                {
                    tin = dr[1].ToString();
                    accNo = dr[3].ToString();
                    tinStaus = dr[4].ToString();

                    var tinStatusName = dbContext.TINStatus.Where(t => t.Id.ToString() == tinStaus).FirstOrDefault();

                    ImportDetail impd = dbContext.ImportDetails.Where(i => i.TIN == tin && i.AccountNo == accNo && i.ImportSummary.Id == imps.Id).FirstOrDefault();
                    if (impd.TINCheckStatus == null)
                    {
                        impd.TINCheckStatus = tinStaus;
                        impd.TINCheckRemarks = tinStatusName.Name;

                        dbContext.Entry(impd).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                result = "TIN matching updated successfully";
            }
            catch(Exception ex)
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
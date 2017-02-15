using Bill2Pay.GenerateIRSFile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bill2Pay.Web.Controllers
{
    public class ImportController : Controller
    {
        ImportUtility utility = null;

        public ImportController()
        {
            utility = new ImportUtility();
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



        public ActionResult Tin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Tin(HttpPostedFileBase fileBase)
        {
            return View();
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
using Bill2Pay.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bill2Pay.GenerateIRSFile;
using System.Web.Hosting;

namespace Bill2Pay.Web.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print

        [HttpPost]
        public ActionResult CopyA(string Id)
        {
            return DetailsReport("CopyA", Id);
        }

        [HttpPost]
        public ActionResult Copy1(string Id)
        {
            return DetailsReport("Copy1", Id);
        }

        [HttpPost]
        public ActionResult CopyB(string Id)
        {
            return DetailsReport("CopyB", Id);
        }

        [HttpPost]
        public ActionResult Copy2(string Id)
        {
            return DetailsReport("Copy2", Id);
        }

        [HttpPost]
        public ActionResult CopyC(string Id)
        {
            return DetailsReport("CopyC", Id);
        }


        public ActionResult PrintAllCopies()
        {
            HostingEnvironment.QueueBackgroundWorkItem(clt => PrintCopies());
            //PrintCopies();
            return RedirectToAction("Index", "IRSProcess");
        }
        public void PrintCopies()
        {
            var rootpath = Server.MapPath(string.Format("~/App_Data/Download/k1099/{0}/", DateTime.Now.Ticks));
            System.IO.Directory.CreateDirectory(rootpath);

            var list = (List<string>)TempData["CheckedMerchantList"];
            //var accno = list[0].ToString();
            foreach (var accno in list)
            {
                var item = ApplicationDbContext.Instence.SubmissionDetails
                   .Include("PSE")
                   .OrderByDescending(p => p.SubmissionId)
                   .FirstOrDefault(p => p.AccountNo.Equals(accno, StringComparison.OrdinalIgnoreCase));

                var data = new List<SubmissionDetail>();
                if (item != null)
                {
                    data.Add(item);
                }

                var pseData = new List<PSEDetails>();
                if (item.PSE != null)
                {
                    pseData.Add(item.PSE);
                }

                string[] reportNames = { "CopyA", "Copy1", "CopyB", "Copy2", "CopyC" };
                LocalReport localReport;
                foreach (var reportName in reportNames)
                {
                    // var reportName = "CopyA";
                    //yield return DetailsReport(reportName, accno);
                    localReport = new LocalReport();
                    localReport.ReportPath = Server.MapPath("~/Reports/" + reportName + ".rdlc");
                    ReportDataSource reportDataSource = new ReportDataSource("SubmissionDetails", data);

                    localReport.DataSources.Add(reportDataSource);

                    ReportDataSource pseDataSource = new ReportDataSource("PSEMaster", pseData);

                    localReport.DataSources.Add(pseDataSource);

                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    //The DeviceInfo settings should be changed based on the reportType
                    //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                    string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.75in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.5in</MarginTop>" +
                    "  <MarginLeft>0.5in</MarginLeft>" +
                    "  <MarginRight>0.5in</MarginRight>" +
                    "  <MarginBottom>0.5in</MarginBottom>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    //Render the report
                    renderedBytes = localReport.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);


                    var path = string.Format("{0}/{1}/", rootpath, accno);
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    path = string.Format("{0}/{1}/{2}.pdf", rootpath, accno, reportName);
                    //File(renderedBytes, mimeType);
                    System.IO.File.WriteAllBytes(path, renderedBytes); // Requires System.IO
                                                                       //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
                }

                ImportUtility.CreateZip(rootpath);
            }

        }

        public ActionResult DetailsReport(string reportName, string Id)
        {

            var item = ApplicationDbContext.Instence.SubmissionDetails
                .Include("PSE")
                .OrderByDescending(p => p.SubmissionId)
                .FirstOrDefault(p => p.AccountNo.Equals(Id, StringComparison.OrdinalIgnoreCase));

            var data = new List<SubmissionDetail>();
            if (item != null)
            {
                data.Add(item);
            }

            var pseData = new List<PSEDetails>();
            if (item.PSE != null)
            {
                pseData.Add(item.PSE);
            }

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/" + reportName + ".rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("SubmissionDetails", data);

            localReport.DataSources.Add(reportDataSource);

            ReportDataSource pseDataSource = new ReportDataSource("PSEMaster", pseData);

            localReport.DataSources.Add(pseDataSource);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.75in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.5in</MarginLeft>" +
            "  <MarginRight>0.5in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }
    }
}
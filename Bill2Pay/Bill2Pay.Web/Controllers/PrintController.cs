using Bill2Pay.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return DetailsReport("CopyC",Id);
        }
        public ActionResult DetailsReport(string reportName, string Id)
        {
            var data = new List<SubmissionDetail>();
            data.Add(new SubmissionDetail()
            {
                AccountNo = "test"
            });//ApplicationDbContext.Instence.SubmissionDetails.FirstOrDefault();)

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/" + reportName + ".rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("SubmissionDetails", data);

            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
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
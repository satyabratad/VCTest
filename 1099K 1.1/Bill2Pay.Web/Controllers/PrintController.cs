using Bill2Pay.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Hosting;
using Bill2Pay.ExceptionLogger;
using System.Configuration;

namespace Bill2Pay.Web.Controllers
{
    public class PrintController : Controller
    {
        /// <summary>
        /// Generates CopyA report for printing
        /// </summary>
        /// <param name="Id">Client Code/ Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CopyA(string Id, int year)
        {
            return DetailsReport("CopyA", Id, year);
        }

        /// <summary>
        /// Generates Copy1 report for printing
        /// </summary>
        /// <param name="Id">Client Code/Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Copy1(string Id, int year)
        {
            return DetailsReport("Copy1", Id, year);
        }

        /// <summary>
        /// Generates CopyB report for printing
        /// </summary>
        /// <param name="Id">Client Code/Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CopyB(string Id, int year)
        {
            return DetailsReport("CopyB", Id, year);
        }

        /// <summary>
        /// Generates Copy2 Report for printing
        /// </summary>
        /// <param name="Id">Client Code/ Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Copy2(string Id, int year)
        {
            return DetailsReport("Copy2", Id, year);
        }

        /// <summary>
        /// Generates CopyC report for printing
        /// </summary>
        /// <param name="Id">Client Code/ Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CopyC(string Id, int year)
        {
            return DetailsReport("CopyC", Id, year);
        }

        /// <summary>
        /// Generates combined 1099K report for printing
        /// </summary>
        /// <param name="Id">Client Code/ Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult IRS1099K(string Id, int year)
        {
            return DetailsReport("1099K", Id, year);
        }

        /// <summary>
        /// This method is for print all the pdf copy of a merchant
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult PrintAllCopies()
        {
            HostingEnvironment.QueueBackgroundWorkItem(clt => PrintCopies());
            return RedirectToAction("Index", "IRSProcess");
        }

        /// <summary>
        /// This method is to run pdf generation in background and download in specified location
        /// </summary>
        public void PrintCopies()
        {
            var downloadPath = ConfigurationManager.AppSettings["DownloaRootPath"];
            if (string.IsNullOrEmpty(downloadPath))
            {
                downloadPath = "~/App_Data/Download/k1099/";
            }
            var errorAccounts = string.Empty;
            var rootpath = Server.MapPath(downloadPath);
            System.IO.Directory.CreateDirectory(rootpath);
            var reportCopyName = string.Empty;
            var list = (List<string>)TempData["PrintableMerchantList"];
            var year = (int)TempData["SelectedYear"];
            string folderName = string.Empty;
            foreach (var accno in list)
            {


                

                var merchantData = ApplicationDbContext.Instence.SubmissionDetails
                  .Include("PSE")
                  .OrderByDescending(p => p.SubmissionId)
                  .Where(s => s.IsActive == true && s.SubmissionSummary.PaymentYear == year)
                  .FirstOrDefault(p => p.AccountNumber.Equals(accno, StringComparison.OrdinalIgnoreCase));

                var status = ApplicationDbContext.Instence.SubmissionStatus
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefault(p => p.AccountNumber.Equals(accno, StringComparison.OrdinalIgnoreCase) && p.IsActive == true && p.PaymentYear == year);

                merchantData.SubmissionType = 0;
                if (status != null)
                {
                    if (status.StatusId == 6)
                    {
                        merchantData.SubmissionType = 1;// Void
                    }
                    if (status.StatusId == 8 || status.StatusId == 4 || status.StatusId == 5)
                    {
                        merchantData.SubmissionType = 2;//Corrected
                    }
                }
                var data = new List<SubmissionDetail>();
                if (merchantData != null)
                {
                    data.Add(merchantData);


                    var pseData = new List<PSEDetails>();
                    if (merchantData.PSE != null)
                    {
                        pseData.Add(merchantData.PSE);
                    }
                    int TransactionYear = merchantData.SubmissionSummary.PaymentYear + 1;
                    folderName = accno + "-" + TransactionYear.ToString();

                    string[] reportNames = { "CopyA", "Copy1", "CopyB", "Copy2", "CopyC" };
                    LocalReport localReport;
                    foreach (var reportName in reportNames)
                    {
                        reportCopyName = accno + "_1099-K_" + reportName + "_" + TransactionYear.ToString();
                        localReport = new LocalReport();
                        localReport.ReportPath = Server.MapPath("~/Reports/" + reportName + ".rdlc");
                        ReportDataSource reportDataSource = new ReportDataSource("SubmissionDetails", data);

                        var yearParam = new ReportParameter("TransactionYear", TransactionYear.ToString());
                        localReport.SetParameters(yearParam);
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


                        var path = string.Format("{0}/{1}/", rootpath, folderName);
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        path = string.Format("{0}/{1}/{2}.pdf", rootpath, folderName, reportCopyName);
                        System.IO.File.WriteAllBytes(path, renderedBytes);
                                                                           
                    }
                    Logger.LogInstance.LogInfo("Pdf file(s) generated for :{0} and the same can be found in {1}{2}", accno, rootpath, folderName);
                }
                else
                {
                    Logger.LogInstance.LogInfo("System unable to generate .pdf file(s) for {0} as the record is not qualified for pdf generation", accno);

                }
               
            }          
        }

        /// <summary>
        /// Public Action Method returns generates pdf report for printing
        /// </summary>
        /// <param name="reportName">Report Name as string</param>
        /// <param name="Id">Client Code/ Account No.</param>
        /// <param name="year">Year</param>
        /// <returns>ActionResult</returns>
        public ActionResult DetailsReport(string reportName, string Id, int year)
        {

            var item = ApplicationDbContext.Instence.SubmissionDetails
                .Include("PSE")
                .OrderByDescending(p => p.SubmissionId)
                .Where(s => s.IsActive == true && s.SubmissionSummary.PaymentYear == year)
                .FirstOrDefault(p => p.AccountNumber.Equals(Id, StringComparison.OrdinalIgnoreCase));

            var status = ApplicationDbContext.Instence.SubmissionStatus
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.AccountNumber.Equals(Id, StringComparison.OrdinalIgnoreCase) && p.IsActive == true && p.PaymentYear == year);

            item.SubmissionType = 0;
            if (status != null)
            {
                if (status.StatusId == 6)
                {
                    item.SubmissionType = 1;// Void
                }
                if (status.StatusId == 8 || status.StatusId == 4 || status.StatusId == 5)
                {
                    item.SubmissionType = 2;//Corrected
                }
            }
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
            int TransactionYear = item.SubmissionSummary.PaymentYear + 1;

            var yearParam = new ReportParameter("TransactionYear", TransactionYear.ToString());
            localReport.SetParameters(yearParam);
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
            var fileName = string.Format("{0}_1099-K_{1}_{2}.pdf", Id, reportName, TransactionYear);
            return File(renderedBytes, mimeType, fileName);
        }
    }
}
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
using Bill2Pay.ExceptionLogger;

namespace Bill2Pay.Web.Controllers
{
    public class PrintController : Controller
    {
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

        [HttpPost]
        public ActionResult IRS1099K(string Id)
        {
            return DetailsReport("1099K", Id);
        }

        public ActionResult PrintAllCopies()
        {
            HostingEnvironment.QueueBackgroundWorkItem(clt => PrintCopies());
            //TempData["successMessage"] = "Generate .pdf file process may take some time. Once completed you can find the files in the '/App_Data/Download/k1099' location. A transaction log is also available. ";
            return RedirectToAction("Index", "IRSProcess");
        }
        public void PrintCopies()
        {
            var errorAccounts = string.Empty;
            var rootpath = Server.MapPath(string.Format("~/App_Data/Download/k1099/"));
            System.IO.Directory.CreateDirectory(rootpath);

            var list = (List<string>)TempData["PrintableMerchantList"];
            var year = (int)TempData["SelectedYear"];
            string folderName = string.Empty;
            //var accno = list[0].ToString();
            foreach (var accno in list)
            {


                folderName = accno + "-" + year.ToString();

                var merchantData = ApplicationDbContext.Instence.SubmissionDetails
                  .Include("PSE")
                  .OrderByDescending(p => p.SubmissionId)
                  .FirstOrDefault(p => p.AccountNumber.Equals(accno, StringComparison.OrdinalIgnoreCase));

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
                    string[] reportNames = { "CopyA", "Copy1", "CopyB", "Copy2", "CopyC" };
                    LocalReport localReport;
                    foreach (var reportName in reportNames)
                    {
                        //var reportName = "CopyA";

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

                        path = string.Format("{0}/{1}/{2}.pdf", rootpath, folderName, reportName);
                        //File(renderedBytes, mimeType);
                        System.IO.File.WriteAllBytes(path, renderedBytes); // Requires System.IO
                                                                           //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);


                    }
                    Logger.LogInstance.LogInfo("Pdf file(s) generated for :{0} and the same can be found in {1}{2}", accno, rootpath, folderName);
                }
                else
                {
                    //errorAccounts = errorAccounts + ", " + accno ;
                    Logger.LogInstance.LogInfo("System unable to generate .pdf file(s) for {0} as the record is not qualified for pdf generation", accno);

                }



                //ImportUtility.CreateZip(rootpath);
            }

            //TempData["successMessage"] = ".pdf file can be found in " + rootpath + " folder. For details see transaction log ";
            //if (errorAccounts.Length > 2)
            //{
            //    errorAccounts = errorAccounts.Substring( errorAccounts.Length - 2);
            //    TempData["errorMessage"] = "System unable to generate .pdf file(s) for :" + errorAccounts + ". For details see transaction log ";
            //}
        }

        public ActionResult DetailsReport(string reportName, string Id)
        {

            var item = ApplicationDbContext.Instence.SubmissionDetails
                .Include("PSE")
                .OrderByDescending(p => p.SubmissionId)
                .Where(s => s.IsActive == true)
                .FirstOrDefault(p => p.AccountNumber.Equals(Id, StringComparison.OrdinalIgnoreCase));

            var status = ApplicationDbContext.Instence.SubmissionStatus
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.AccountNumber.Equals(Id, StringComparison.OrdinalIgnoreCase) && p.IsActive == true);

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
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
            var fileName = string.Format("{0}_1099-K_{1}_{2}.pdf",Id, reportName, TransactionYear);
            return File(renderedBytes, mimeType, fileName);
        }
    }
}
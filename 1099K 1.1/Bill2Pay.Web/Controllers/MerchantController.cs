using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bill2Pay.Model;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using Newtonsoft.Json;
using System.Transactions;
using Bill2Pay.GenerateIRSFile;

namespace Bill2Pay.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MerchantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Dictionary<string, string> GetpaymentIndicator()
        {
            Dictionary<string, string> IndicatorList = new Dictionary<string, string>();

            IndicatorList.Add("Payment Card Payment	", "1");
            IndicatorList.Add("Third Party Network Payment	", "2");
            return IndicatorList;
        }

        private Dictionary<string, string> GetTINTypes()
        {
            Dictionary<string, string> TINTypeList = new Dictionary<string, string>();

            TINTypeList.Add("EIN", "1");
            TINTypeList.Add("SSN / ITIN / ATIN", "2");
            return TINTypeList;
        }

        private Dictionary<string, string> GetFilerIndicatore()
        {
            Dictionary<string, string> FilerList = new Dictionary<string, string>();

            FilerList.Add("Payment Settlement Entity(PSE)", "1");
            FilerList.Add("Electronic PaymentFacilitator(EPF) / Other third party", "2");

            return FilerList;

        }
        private Dictionary<string, string> GetStateList()
        {
            Dictionary<string, string> StateList = new Dictionary<string, string>();

            StateList.Add("Alabama	", "AL");
            StateList.Add("Alaska	", "AK");
            StateList.Add("American Samoa", "AS");
            StateList.Add("Arizona	", "AZ");
            StateList.Add("Arkansas	", "AR");
            StateList.Add("California	", "CA");
            StateList.Add("Colorado	", "CO");
            StateList.Add("Connecticut	", "CT");
            StateList.Add("Delaware	", "DE");
            StateList.Add("District of Columbia	", "DC");
            StateList.Add("Florida	", "FL");
            StateList.Add("Georgia	", "GA");
            StateList.Add("Guam	", "GU");
            StateList.Add("Hawaii	", "HI");
            StateList.Add("Idaho	", "ID");
            StateList.Add("Illinois	", "IL");
            StateList.Add("Indiana	", "IN");
            StateList.Add("Iowa	", "	IA");
            StateList.Add("Kansas	", "KS");
            StateList.Add("Kentucky	", "KY");
            StateList.Add("Louisiana	", "LA");
            StateList.Add("Maine	", "ME");
            StateList.Add("Maryland	", "MD");
            StateList.Add("Massachusetts", "MA");
            StateList.Add("Michigan	", "MI");
            StateList.Add("Minnesota	", "MN");
            StateList.Add("Mississippi	", "MS");
            StateList.Add("Missouri	", "MO");
            StateList.Add("Montana	", "MT");
            StateList.Add("Nebraska	", "NE");
            StateList.Add("Nevada	", "NV");
            StateList.Add("New Hampshire	", "NH");
            StateList.Add("New Jersey	", "NJ");
            StateList.Add("New Mexico	", "NM");
            StateList.Add("New York	", "NY");
            StateList.Add("North Carolina	", "NC");
            StateList.Add("North Dakota	", "ND");
            StateList.Add("No. Mariana Islands	", "MP");
            StateList.Add("Ohio	", "OH");
            StateList.Add("Oklahoma	", "OK");
            StateList.Add("Oregon	", "OR");
            StateList.Add("Pennsylvania	", "PA");
            StateList.Add("Puerto Rico	", "PR");
            StateList.Add("Rhode Island	", "RI");
            StateList.Add("South Carolina	", "SC");
            StateList.Add("South Dakota	", "SD");
            StateList.Add("Tennessee	", "TN");
            StateList.Add("Texas	", "TX");
            StateList.Add("Utah	", "UT");
            StateList.Add("Vermont	", "VT");
            StateList.Add("Virginia	", "VA");
            StateList.Add("U.S. Virgin Islands	", "VI");
            StateList.Add("Washington	", "WA");
            StateList.Add("West Virginia	", "WV");
            StateList.Add("Wisconsin	", "WI");
            StateList.Add("Wyoming	", "WY");





            return StateList;
        }

        public ActionResult Index(int? Id, int? payer)
        {
            int year = 0;
            List<MerchantDetailsVM> merchants;
            if (payer == null)
            {
                payer = 0;
            }

            if (Id == null)
            {
                year = DateTime.Now.Year - 1;

            }
            else
            {
                year = (int)Id;
            }
            merchants = db.MerchantDetails.Include(m => m.CreatedUser).Include(m => m.Payer)
                                   .GroupJoin(db.SubmissionStatus.Where(s => s.IsActive == true && s.PaymentYear == year),
                                    mer => mer.PayeeAccountNumber,
                                    stat => stat.AccountNumber,
                                    (mer, stat) => new MerchantDetailsVM() { Merchant = mer, Status = stat.FirstOrDefault() })
                                        .Where(m => m.Merchant.IsActive == true && (payer == 0 || m.Merchant.PayerId == payer))
                                        .OrderBy(m => m.Merchant.FirstPayeeName).ToList();

            var payerlst = db.PayerDetails.Where(p => p.IsActive == true)
                .Select(p => new SelectListItem() { Text = p.FirstPayerName, Value = p.Id.ToString() }).OrderBy(p => p.Text).ToList();
            ViewBag.PayerList = payerlst;
            ViewBag.SelectedYear = year;
            return View(merchants.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantDetails merchantDetails = db.MerchantDetails.Find(id);
            if (merchantDetails == null)
            {
                return HttpNotFound();
            }
            return View(merchantDetails);
        }

        public ActionResult Create()
        {
            var statelst = GetStateList().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.StateList = statelst;
            var IndicatorLst = GetpaymentIndicator().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.PaymentIndicatorList = IndicatorLst;

            var filerLst = GetFilerIndicatore().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.FilerList = filerLst;

            var TINTypeLst = GetTINTypes().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.TINTypeLstList = TINTypeLst;


            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MerchantDetails merchantDetails)
        {
            if (ModelState.IsValid)
            {
                var merdet = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase) && m.IsActive == true).FirstOrDefault();
                if (merdet != null)
                {
                    ViewBag.StatusMessage = "Client Code already exists.";

                    var statelst = GetStateList().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                    ViewBag.StateList = statelst;
                    var IndicatorLst = GetpaymentIndicator().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                    ViewBag.PaymentIndicatorList = IndicatorLst;

                    var filerLst = GetFilerIndicatore().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                    ViewBag.FilerList = filerLst;

                    var TINTypeLst = GetTINTypes().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                    ViewBag.TINTypeLstList = TINTypeLst;


                    ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName");
                    return View(merchantDetails);
                }
                merchantDetails.DateAdded = System.DateTime.Now;
                merchantDetails.UserId = int.Parse(User.Identity.GetUserId());
                merchantDetails.IsActive = true;
                db.MerchantDetails.Add(merchantDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName", merchantDetails.PayerId);
            return View(merchantDetails);
        }

        public ActionResult Edit(string id, int? year)
        {
            ViewBag.StatusMessage = string.Empty;
            var returnUrl = HttpContext.Request.UrlReferrer;
            TempData["MerchantEditreturnUrl"] = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (year == null)
            {
                year = System.DateTime.Now.Year - 1;
            }

            MerchantDetails merchantDetails = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                           && m.IsActive == true).FirstOrDefault();

            if (merchantDetails == null)
            {
                return HttpNotFound();
            }

            var subStatus = db.SubmissionStatus.Where(s => s.AccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                                  && s.IsActive == true && s.PaymentYear == year).FirstOrDefault();
            if (subStatus != null)
            {
                switch (subStatus.StatusId)
                {
                    case 6:
                    case 3:
                    case 4:
                    case 5:
                    case 8:
                    case 9:
                        ViewBag.StatusMessage = "1099K is already submitted for the merchant, Updated information will not reflect in IRS until next submission.";
                        break;
                    case 7:
                        ViewBag.StatusMessage = "This merchant is marked as 'Two-Transaction Correction', Updated information will reflect in re-submission.";
                        break;
                }
            }

            var statelst = GetStateList().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.StateList = statelst;
            var IndicatorLst = GetpaymentIndicator().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.PaymentIndicatorList = IndicatorLst;

            var filerLst = GetFilerIndicatore().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.FilerList = filerLst;

            var TINTypeLst = GetTINTypes().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.TINTypeLstList = TINTypeLst;

            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName", merchantDetails.PayerId);
            ModelState.Clear();
            return View(merchantDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MerchantDetails merchantDetails)
        {
            var merdet = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase) 
            && m.IsActive == true
            && m.Id != merchantDetails.Id).FirstOrDefault();
            if (merdet != null)
            {
                ViewBag.StatusMessage = "Client Code already exists.";

                var statelst = GetStateList().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                ViewBag.StateList = statelst;
                var IndicatorLst = GetpaymentIndicator().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                ViewBag.PaymentIndicatorList = IndicatorLst;

                var filerLst = GetFilerIndicatore().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                ViewBag.FilerList = filerLst;

                var TINTypeLst = GetTINTypes().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
                ViewBag.TINTypeLstList = TINTypeLst;


                ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName");
                return View(merchantDetails);
            }

            if (ModelState.IsValid)
            {

                var returnUrl = TempData["MerchantEditreturnUrl"].ToString();

                using (TransactionScope scope = new TransactionScope())
                {

                    MerchantDetails Merchant = db.MerchantDetails.Find(merchantDetails.Id);
                    Merchant.IsActive = false;

                    db.Entry(Merchant).State = EntityState.Modified;
                    db.SaveChanges();

                    merchantDetails.DateAdded = System.DateTime.Now;
                    merchantDetails.UserId = int.Parse(User.Identity.GetUserId());
                    merchantDetails.IsActive = true;
                    db.MerchantDetails.Add(merchantDetails);
                    db.SaveChanges();



                    var iDet = db.ImportDetails.Where(i => i.IsActive == true && i.AccountNumber.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase))
                                .GroupJoin(db.SubmissionStatus.Where(s => s.IsActive == true && s.AccountNumber.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase)),
                                imp => new { acc = imp.AccountNumber, year = imp.ImportSummary.PaymentYear },
                                stat => new { acc = stat.AccountNumber, year = stat.PaymentYear },
                                (imp, stat) => new MerchantListVM() { ImportDetails = imp, SubmissionStatus = stat.FirstOrDefault() })
                                .ToList();

                    if (iDet != null)
                    {
                        foreach (MerchantListVM impdstat in iDet)
                        {
                            if (impdstat.SubmissionStatus == null || impdstat.SubmissionStatus.StatusId <= (int)RecordStatus.FileGenerated || impdstat.SubmissionStatus.StatusId == (int)RecordStatus.TwoTransactionCorrection)
                            {
                                ImportDetail newimpdet = impdstat.ImportDetails;
                                ImportDetail impdet = impdstat.ImportDetails;

                                impdet.IsActive = false;

                                db.Entry(impdet).State = EntityState.Modified;
                                db.SaveChanges();

                                newimpdet.IsActive = true;
                                newimpdet.AccountNumber = merchantDetails.PayeeAccountNumber;
                                newimpdet.TIN = merchantDetails.PayeeTIN;
                                newimpdet.FirstPayeeName = merchantDetails.FirstPayeeName;
                                newimpdet.SecondPayeeName = merchantDetails.SecondPayeeName;
                                newimpdet.PayeeMailingAddress = merchantDetails.PayeeMailingAddress;
                                newimpdet.PayeeCity = merchantDetails.PayeeCity;
                                newimpdet.PayeeState = merchantDetails.PayeeState;
                                newimpdet.PayeeZipCode = merchantDetails.PayeeZIP;
                                newimpdet.FillerIndicatorType = merchantDetails.FilerIndicatorType;
                                newimpdet.PaymentIndicatorType = merchantDetails.PaymentIndicatorType;
                                newimpdet.MerchantCategoryCode = merchantDetails.MCC;
                                newimpdet.CFSF = merchantDetails.CFSF;
                                newimpdet.MerchantId = merchantDetails.Id;
                                db.ImportDetails.Add(newimpdet);

                                db.SaveChanges();
                                if (impdstat.SubmissionStatus != null)
                                {

                                    SubmissionDetail subdet = db.SubmissionDetails
                                            .Include("SubmissionSummary")
                                            .Where(s => s.SubmissionSummary.Id == newimpdet.SubmissionSummaryId && s.IsActive == true
                                                 && s.AccountNumber.Equals(newimpdet.AccountNumber, StringComparison.InvariantCultureIgnoreCase)
                                                 && s.SubmissionSummary.PaymentYear == impdstat.SubmissionStatus.PaymentYear).FirstOrDefault();


                                    SubmissionDetail newsubdet = subdet;

                                    subdet.IsActive = false;

                                    db.Entry(subdet).State = EntityState.Modified;
                                    db.SaveChanges();

                                    newsubdet.IsActive = true;
                                    newsubdet.AccountNumber = merchantDetails.PayeeAccountNumber;
                                    newsubdet.TIN = merchantDetails.PayeeTIN;
                                    newsubdet.FirstPayeeName = merchantDetails.FirstPayeeName;
                                    newsubdet.SecondPayeeName = merchantDetails.SecondPayeeName;
                                    newsubdet.PayeeMailingAddress = merchantDetails.PayeeMailingAddress;
                                    newsubdet.PayeeCity = merchantDetails.PayeeCity;
                                    newsubdet.PayeeState = merchantDetails.PayeeState;
                                    newsubdet.PayeeZipCode = merchantDetails.PayeeZIP;
                                    newsubdet.FillerIndicatorType = merchantDetails.FilerIndicatorType;
                                    newsubdet.PaymentIndicatorType = merchantDetails.PaymentIndicatorType;
                                    newsubdet.MerchantCategoryCode = merchantDetails.MCC;
                                    newsubdet.CFSF = merchantDetails.CFSF;
                                    newsubdet.MerchantId = merchantDetails.Id;
                                    db.SubmissionDetails.Add(newsubdet);
                                    db.SaveChanges();

                                    SubmissionStatus status = impdstat.SubmissionStatus;
                                    SubmissionStatus newStatus = status;
                                    status.IsActive = false;

                                    db.Entry(status).State = EntityState.Modified;
                                    db.SaveChanges();

                                    newStatus.IsActive = true;
                                    newStatus.StatusId = (int)RecordStatus.TwoTransactionUploaded;
                                    newStatus.DateAdded = System.DateTime.Now;
                                    db.SubmissionStatus.Add(newStatus);
                                    db.SaveChanges();
                                }
                            }
                        }

                        scope.Complete();
                    }

                    return Redirect(returnUrl);
                }
            }

            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF", merchantDetails.PayerId);
            return View(merchantDetails);

        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantDetails merchantDetails = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                            && m.IsActive == true).FirstOrDefault();
            if (merchantDetails == null)
            {
                return HttpNotFound();
            }
            return View(merchantDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MerchantDetails merchantDetails = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                           && m.IsActive == true).FirstOrDefault();
            if (merchantDetails != null)
            {
                merchantDetails.IsActive = false;
                db.Entry(merchantDetails).State = EntityState.Modified;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

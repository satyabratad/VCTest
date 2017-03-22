﻿using System;
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

namespace Bill2Pay.Web.Controllers
{
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

        private Dictionary<string, string>  GetFilerIndicatore()
        {
            Dictionary<string, string> FilerList = new Dictionary<string, string>();

            FilerList.Add("Payment Settlement Entity(PSE)", "1");
            FilerList.Add("Electronic PaymentFacilitator(EPF) / Other third party", "2");

            return FilerList;

        }
        private Dictionary<string,string>  GetStateList()
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


        // GET: Merchant
        public ActionResult Index(int? Id,int? payer)
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
                return RedirectToAction("Index", new { id = year, payer = payer });
            }
            else
            {
                year =(int) Id;
            }
            merchants = db.MerchantDetails.Include(m => m.CreatedUser).Include(m => m.Payer)
                                   .GroupJoin(db.SubmissionStatus.Where(s=>s.IsActive==true && s.PaymentYear==year),
                                    mer=>mer.PayeeAccountNumber,
                                    stat=>stat.AccountNumber,
                                    (mer,stat)=> new MerchantDetailsVM() { Merchant = mer, Status = stat.FirstOrDefault() }) 
                                        .Where(m => m.Merchant.IsActive == true && (payer==0 ||m.Merchant.PayerId==payer) && m.Merchant.PaymentYear==year)
                                        .OrderBy(m=>m.Merchant.PayeeFirstName).ToList() ;

            var payerlst = db.PayerDetails.Where(p => p.IsActive == true)
                .Select(p => new SelectListItem() { Text = p.FirstPayerName, Value =p.Id.ToString() }).OrderBy(p => p.Text).ToList();
            ViewBag.PayerList = payerlst;
            ViewBag.SelectedYear = year;
            return View(merchants.ToList());
        }

        // GET: Merchant/Details/5
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

        // GET: Merchant/Create
        public ActionResult Create()
        {
            var statelst = GetStateList().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.StateList = statelst;
            var IndicatorLst = GetpaymentIndicator().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.PaymentIndicatorList = IndicatorLst;

            var filerLst= GetFilerIndicatore().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.FilerList = filerLst;

            var TINTypeLst = GetTINTypes().Select(s => new SelectListItem() { Text = s.Key, Value = s.Value }).ToList();
            ViewBag.TINTypeLstList = TINTypeLst;
            
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName");
            return View();
        }

        // POST: Merchant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "Id,PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId,PaymentYear")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MerchantDetails merchantDetails)
        {
            if (ModelState.IsValid)
            {
                merchantDetails.DateAdded = System.DateTime.Now;
                merchantDetails.UserId =int.Parse(User.Identity.GetUserId());
                merchantDetails.PaymentYear = System.DateTime.Now.Year;
                merchantDetails.IsActive = true;
                db.MerchantDetails.Add(merchantDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName", merchantDetails.PayerId);
            return View(merchantDetails);
        }

        // GET: Merchant/Edit/5
        public ActionResult Edit(string id,int? year)
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
            MerchantDetails merchantDetails = db.MerchantDetails.Where(m=>m.PayeeAccountNumber.Equals(id,StringComparison.InvariantCultureIgnoreCase)
                                            && m.IsActive==true && m.PaymentYear== year).FirstOrDefault();
            if (merchantDetails == null)
            {
                return HttpNotFound();
            }

            var subStatus = db.SubmissionStatus.Where(s => s.AccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                                  && s.IsActive == true && s.PaymentYear == year).FirstOrDefault();
            if(subStatus != null)
            {
                switch (subStatus.StatusId)
                {
                    case 6: case 3: case 4:
                    case 5: case 8: case 9:
                        ViewBag.StatusMessage = " Data of this merchant is already submitted for this year's 1099K. Updated information will effect on next year's 1099K data.   ";
                        break;
                    case 7:
                        ViewBag.StatusMessage = " This merchant data is for Two transaction correction. Corrected data will be effect on 1099K data.   ";
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
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "FirstPayerName", merchantDetails.PayerId);
            ModelState.Clear();
            return View(merchantDetails);
        }

        // POST: Merchant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MerchantDetails merchantDetails)
        {
            if (ModelState.IsValid)
            {
                //MerchantDetails Merchant = merchantDetails;
                var returnUrl = TempData["MerchantEditreturnUrl"].ToString();
               



                var status = db.SubmissionStatus.Where(s => s.AccountNumber.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase)
                                                  && s.PaymentYear == merchantDetails.PaymentYear  && s.IsActive==true).FirstOrDefault();

                MerchantDetails Merchant = db.MerchantDetails.Find(merchantDetails.Id);
                Merchant.IsActive = false;

                db.Entry(Merchant).State = EntityState.Modified;
                db.SaveChanges();

                merchantDetails.DateAdded = System.DateTime.Now;
                merchantDetails.UserId = int.Parse(User.Identity.GetUserId());
                //merchantDetails.PaymentYear = System.DateTime.Now.Year;
                merchantDetails.IsActive = true;
                db.MerchantDetails.Add(merchantDetails);
                db.SaveChanges();


                ImportDetail impdet = db.ImportDetails.Where(i => i.AccountNo.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase)
                                            && i.IsActive == true && i.ImportSummary.PaymentYear == merchantDetails.PaymentYear).FirstOrDefault();
                if (impdet != null)
                {
                    ImportDetail newimpdet = impdet;
                    impdet.IsActive = false;

                    db.Entry(impdet).State = EntityState.Modified;
                    db.SaveChanges();

                    newimpdet.IsActive = true;
                    newimpdet.AccountNo = merchantDetails.PayeeAccountNumber;
                    newimpdet.TIN = merchantDetails.PayeeTIN;
                    newimpdet.FirstPayeeName = merchantDetails.PayeeFirstName;
                    newimpdet.SecondPayeeName = merchantDetails.PayeeSecondName;
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
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }

                if (status !=null)
                {
                    if (status.StatusId == 7)// Two transaction Correction
                    {
                        

                        SubmissionDetail subdet = db.SubmissionDetails.Where(i => i.AccountNo.Equals(merchantDetails.PayeeAccountNumber, StringComparison.InvariantCultureIgnoreCase)
                                                && i.IsActive == true && i.SubmissionSummary.PaymentYear == merchantDetails.PaymentYear).FirstOrDefault();

                        SubmissionDetail newsubdet = subdet;

                        subdet.IsActive = false;

                        db.Entry(subdet).State = EntityState.Modified;
                        db.SaveChanges();

                        newsubdet.IsActive = true;
                        newsubdet.AccountNo = merchantDetails.PayeeAccountNumber;
                        newsubdet.TIN = merchantDetails.PayeeTIN;
                        newsubdet.FirstPayeeName = merchantDetails.PayeeFirstName;
                        newsubdet.SecondPayeeName = merchantDetails.PayeeSecondName;
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
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }

                        SubmissionStatus newStatus = status;
                        status.IsActive = false;

                        db.Entry(status).State = EntityState.Modified;
                        db.SaveChanges();

                        newStatus.IsActive = true;
                        newStatus.StatusId = 8; // TODO: status will get from enum
                        newStatus.DateAdded = System.DateTime.Now;
                        db.SubmissionStatus.Add(newStatus);
                        db.SaveChanges();

                    }

                }


                //return RedirectToAction("Index");
                return Redirect(returnUrl);
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF", merchantDetails.PayerId);
            return View(merchantDetails);
            
        }

        // GET: Merchant/Delete/5
        public ActionResult Delete(string id,int? year)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (year == null)
            {
                year = System.DateTime.Now.Year - 1;
            }
            MerchantDetails merchantDetails = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                            && m.IsActive == true && m.PaymentYear == year).FirstOrDefault();
            if (merchantDetails == null)
            {
                return HttpNotFound();
            }
            return View(merchantDetails);
        }

        // POST: Merchant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, int? year)
        {
            MerchantDetails merchantDetails = db.MerchantDetails.Where(m => m.PayeeAccountNumber.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                                           && m.IsActive == true && m.PaymentYear == year).FirstOrDefault();
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

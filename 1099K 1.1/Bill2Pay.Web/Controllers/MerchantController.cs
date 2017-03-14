using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bill2Pay.Model;

namespace Bill2Pay.Web.Controllers
{
    public class MerchantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Merchant
        public ActionResult Index(int? year)
        {
            var merchantDetails = db.MerchantDetails.Include(m => m.CreatedUser).Include(m => m.Payer)
                                    .Where(m=>m.IsActive==true) ;
            return View(merchantDetails.ToList());
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
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF");
            return View();
        }

        // POST: Merchant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId,PaymentYear")] MerchantDetails merchantDetails)
        {
            if (ModelState.IsValid)
            {
                db.MerchantDetails.Add(merchantDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF", merchantDetails.PayerId);
            return View(merchantDetails);
        }

        // GET: Merchant/Edit/5
        public ActionResult Edit(int? id)
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
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF", merchantDetails.PayerId);
            return View(merchantDetails);
        }

        // POST: Merchant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId,PaymentYear")] MerchantDetails merchantDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", merchantDetails.UserId);
            ViewBag.PayerId = new SelectList(db.PayerDetails, "Id", "CFSF", merchantDetails.PayerId);
            return View(merchantDetails);
        }

        // GET: Merchant/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Merchant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MerchantDetails merchantDetails = db.MerchantDetails.Find(id);
            db.MerchantDetails.Remove(merchantDetails);
            db.SaveChanges();
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

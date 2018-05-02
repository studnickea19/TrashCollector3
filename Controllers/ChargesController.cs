using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector3.Models;

namespace TrashCollector3.Controllers
{
    public class ChargesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Customer Pickups
        public ActionResult MyCharges(Customer customer)
        {
            var pickups = db.Customers.Include(c => c.Charges);
            return View();
        }

        // GET: Charges
        public ActionResult Index()
        {
            var charges = db.Charges.Include(c => c.Customer).Include(c => c.Pickup);
            return View(charges.ToList());
        }

        // GET: Charges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // GET: Charges/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.PickupID = new SelectList(db.Pickups, "PickupID", "PickupID");
            return View();
        }

        // POST: Charges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChargeID,ChargeAmount,PickupID,CustomerID")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Charges.Add(charge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", charge.CustomerID);
            ViewBag.PickupID = new SelectList(db.Pickups, "PickupID", "PickupID", charge.PickupID);
            return View(charge);
        }

        // GET: Charges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", charge.CustomerID);
            ViewBag.PickupID = new SelectList(db.Pickups, "PickupID", "PickupID", charge.PickupID);
            return View(charge);
        }

        // POST: Charges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChargeID,ChargeAmount,PickupID,CustomerID")] Charge charge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(charge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", charge.CustomerID);
            ViewBag.PickupID = new SelectList(db.Pickups, "PickupID", "PickupID", charge.PickupID);
            return View(charge);
        }

        // GET: Charges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Charge charge = db.Charges.Find(id);
            if (charge == null)
            {
                return HttpNotFound();
            }
            return View(charge);
        }

        // POST: Charges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Charge charge = db.Charges.Find(id);
            db.Charges.Remove(charge);
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

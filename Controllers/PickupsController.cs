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
    public class PickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Employee pickups
        public ActionResult EmployeePickups(Customer customer)
        {
            var pickups = db.Customers.Include(p => p.PickUps);
            return View();
        }

        //GET: Customer Pickups
        public ActionResult MyPickups(Customer customer)
        {
            var pickups = db.Customers.Include(p => p.PickUps);
            return View();
        }
        // GET: Pickups
        public ActionResult Index()
        {
            var pickups = db.Pickups.Include(p => p.CustomerAddress).Include(p => p.PickUpArea);
            return View(pickups.ToList());
        }

        // GET: Pickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // GET: Pickups/Create
        public ActionResult Create()
        {
            ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID");
            ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID");
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupID,CustomerAddressID,PickUpDate,AreaID,PickupCompleted,PickupSuspended,OneTimePickup")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                pickup.PickupCompleted = false;
                pickup.PickupSuspended = false;
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID", pickup.CustomerAddressID);
            ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID", pickup.AreaID);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID", pickup.CustomerAddressID);
            ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID", pickup.AreaID);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickupID,CustomerAddressID,PickUpDate,AreaID,PickupCompleted,PickupSuspended,OneTimePickup")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID", pickup.CustomerAddressID);
            ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID", pickup.AreaID);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            db.Pickups.Remove(pickup);
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

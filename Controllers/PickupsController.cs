using Microsoft.AspNet.Identity;
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
        public ActionResult MyPickups()
        {
            string userID = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userID).FirstOrDefault();
            var customer = db.Customers.Where(c => c.UserID == user.Id).FirstOrDefault();
            var customerPickups = db.Customers.Where(c => c.CustomerID == customer.CustomerID).Include(p => p.PickUps).FirstOrDefault();
            var pickups = customerPickups.PickUps;
            //var addresses = db.Customers.Include(a => a.UserAddresses);
            return View(pickups);
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
            //ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID");
            //ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID");
            //return View();
            string userID = User.Identity.GetUserId();  //get current userID
            var user = db.Users.Where(u => u.Id == userID).FirstOrDefault();    //get user 
            var customer = db.Customers.Where(c => c.UserID == user.Id).FirstOrDefault();   //get customer that matches user
            List<Address> addresses = db.CustomerAddresses.Where(c => c.CustomerID == customer.CustomerID).Select(a => a.Address).ToList(); //create list of addresses
            Pickup pickup = new Pickup()            //create new ppickup
            {                                       //Pickups's list of addresses is equal to one created above
                Addresses = addresses
            };
            return View(pickup);
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
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("MyPickups", "PickupController");
            }

            //ViewBag.CustomerAddressID = new SelectList(db.CustomerAddresses, "CustomerAddressID", "CustomerAddressID", pickup.CustomerAddressID);
            //ViewBag.AreaID = new SelectList(db.PickUpAreas, "AreaID", "AreaID", pickup.AreaID);
            return View(pickup);
            //if (pickup.PickupID < 0)              
            //{
            //    db.Pickups.Add(pickup);   
            //}
            //else
            //{
                
            //    var pickupInDB = db.Pickups.Single(m => m.PickupID == pickup.PickupID); //
            //    pickupInDB.CustomerAddress = pickup.CustomerAddress;
            //    pickupInDB.PickUpDate = pickup.PickUpDate;
            //    pickupInDB.PickUpArea = pickup.PickUpArea;
            //    pickupInDB.PickupCompleted = false;
            //    pickupInDB.PickupSuspended = false;
            //    pickupInDB.OneTimePickup = pickup.OneTimePickup;
                    
            //}
            //db.SaveChanges();
            
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

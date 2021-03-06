﻿using Microsoft.AspNet.Identity;
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
    public class AddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Addresses
        public ActionResult Index()
        {
            
            return View(db.Addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        public ActionResult MyAddresses()
        {
            string userID = User.Identity.GetUserId();  //get current userID
            var user = db.Users.Where(u => u.Id == userID).FirstOrDefault();    //get user 
            var customer = db.Customers.Where(c => c.UserID == user.Id).FirstOrDefault();   //get customer that matches user
            List<Address> addresses = db.CustomerAddresses.Where(c => c.CustomerID == customer.CustomerID).Select(a => a.Address).ToList();
            return View(addresses);
        }
        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressID,StreetAddress,City,State,ZipCode")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                string userID = User.Identity.GetUserId();  //get current userID
                var user = db.Users.Where(u => u.Id == userID).FirstOrDefault();    //get user 
                var customer = db.Customers.Where(c => c.UserID == user.Id).FirstOrDefault();   //get customer that matches user
                CustomerAddress addAddress = new CustomerAddress();
                addAddress.AddressID = address.AddressID;
                addAddress.CustomerID = customer.CustomerID;
                db.CustomerAddresses.Add(addAddress);
                db.SaveChanges();
                return RedirectToAction("MyAddresses", "Addresses");
            }

            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressID,StreetAddress,City,State,ZipCode")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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

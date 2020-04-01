using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using practice.Models;
using practice.Models.APIModels;

namespace practice.Controllers
{
    [Authorize(Roles = "Sibling")]
    public class UnavailableDatesController : Controller
    {
        private ProjectExampleEntities db = new ProjectExampleEntities();

        // GET: UnavailableDates
        public ActionResult Index()
        {
            return RedirectToAction("Calendar", "Home");
           
        }
        public ActionResult MyDates()
        {
            var userid = User.Identity.GetUserId();
            var sibling = db.Siblings.Find(userid);
            var UnavailableDates = sibling.UnavailableDates;
            return View(UnavailableDates.ToList());

        }

        // GET: UnavailableDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnavailableDate unavailableDate = db.UnavailableDates.Find(id);
            if (unavailableDate == null)
            {
                return HttpNotFound();
            }
            return View(unavailableDate);
        }

        // GET: UnavailableDates/Create
        public ActionResult Create()
        {
            var model = new UnavailableDate
            {
                SiblingId = User.Identity.GetUserId(),
                Busydate = DateTime.Today
            };
            return View(model);
        }

        // POST: UnavailableDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SiblingId,Busydate")] UnavailableDate unavailableDate)
        {
            if (ModelState.IsValid)
            {
                db.UnavailableDates.Add(unavailableDate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiblingId = new SelectList(db.Siblings, "Id", "FirstName", unavailableDate.SiblingId);
            return View(unavailableDate);
        }

        // GET: UnavailableDates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnavailableDate unavailableDate = db.UnavailableDates.Find(id);
            if (unavailableDate == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiblingId = new SelectList(db.Siblings, "Id", "FirstName", unavailableDate.SiblingId);
            return View(unavailableDate);
        }

        // POST: UnavailableDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SiblingId,Busydate")] UnavailableDate unavailableDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unavailableDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyDates");
            }
            ViewBag.SiblingId = new SelectList(db.Siblings, "Id", "FirstName", unavailableDate.SiblingId);
            return View(unavailableDate);
        }

        // GET: UnavailableDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnavailableDate unavailableDate = db.UnavailableDates.Find(id);
            if (unavailableDate == null)
            {
                return HttpNotFound();
            }
            return View(unavailableDate);
        }

        // POST: UnavailableDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnavailableDate unavailableDate = db.UnavailableDates.Find(id);
            db.UnavailableDates.Remove(unavailableDate);
            db.SaveChanges();
            return RedirectToAction("MyDates");
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

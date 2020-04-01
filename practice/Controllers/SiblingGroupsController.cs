using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using practice.Models;

namespace practice.Controllers
{
    public class SiblingGroupsController : Controller
    {
        private ProjectExampleEntities db = new ProjectExampleEntities();

        // GET: SiblingGroups
        public ActionResult Index()
        {
            return View(db.SiblingGroups.ToList());
        }

        // GET: SiblingGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiblingGroup siblingGroup = db.SiblingGroups.Find(id);
            if (siblingGroup == null)
            {
                return HttpNotFound();
            }
            return View(siblingGroup);
        }

        // GET: SiblingGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiblingGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SiblingGroup siblingGroup)
        {
            if (ModelState.IsValid)
            {
                db.SiblingGroups.Add(siblingGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siblingGroup);
        }

        // GET: SiblingGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiblingGroup siblingGroup = db.SiblingGroups.Find(id);
            if (siblingGroup == null)
            {
                return HttpNotFound();
            }
            return View(siblingGroup);
        }

        // POST: SiblingGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SiblingGroup siblingGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siblingGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siblingGroup);
        }

        // GET: SiblingGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiblingGroup siblingGroup = db.SiblingGroups.Find(id);
            if (siblingGroup == null)
            {
                return HttpNotFound();
            }
            return View(siblingGroup);
        }

        // POST: SiblingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiblingGroup siblingGroup = db.SiblingGroups.Find(id);
            db.SiblingGroups.Remove(siblingGroup);
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

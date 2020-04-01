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
    public class SiblingsController : Controller
    {
        private ProjectExampleEntities db = new ProjectExampleEntities();

        // GET: Siblings
        public ActionResult Index()
        {
            var siblings = db.Siblings.Include(s => s.SiblingGroup);
            return View(siblings.ToList());
        }

        // GET: Siblings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sibling sibling = db.Siblings.Find(id);
            if (sibling == null)
            {
                return HttpNotFound();
            }
            return View(sibling);
        }

        // GET: Siblings/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name");
            return View();
        }

        // POST: Siblings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupId,FirstName,LastName,Address,PhoneNumber,Email")] Sibling sibling)
        {
            if (ModelState.IsValid)
            {
                db.Siblings.Add(sibling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", sibling.GroupId);
            return View(sibling);
        }

        // GET: Siblings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sibling sibling = db.Siblings.Find(id);
            if (sibling == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", sibling.GroupId);
            return View(sibling);
        }

        // POST: Siblings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupId,FirstName,LastName,Address,PhoneNumber,Email")] Sibling sibling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sibling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", sibling.GroupId);
            return View(sibling);
        }

        // GET: Siblings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sibling sibling = db.Siblings.Find(id);
            if (sibling == null)
            {
                return HttpNotFound();
            }
            return View(sibling);
        }

        // POST: Siblings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sibling sibling = db.Siblings.Find(id);
            db.Siblings.Remove(sibling);
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

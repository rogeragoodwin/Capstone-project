using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using practice.Models;

namespace practice.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private ProjectExampleEntities db = new ProjectExampleEntities();

        // GET: Visits
        public ActionResult Index(int? id)
        {
            SiblingGroup siblingGroup;
            if (User.IsInRole("Driver"))
            {
                siblingGroup = db.SiblingGroups.Find(id);
            }
            else
            {
                return RedirectToAction("Calendar", "Home");
                var userid = User.Identity.GetUserId();
                var Sibling = db.Siblings.Find(userid);
                siblingGroup = Sibling.SiblingGroup;

            }

            var visits = siblingGroup.Visits;
            return View(visits.ToList());
        }
        public ActionResult GroupVisits()
        {
            var userid = User.Identity.GetUserId();
            IEnumerable<Visit> Visits;
            if (User.IsInRole("Driver"))
            {
                var Driver = db.Drivers.Find(userid);
                Visits = Driver.Visits;
            }
            else
            {
              
               
                var Sibling = db.Siblings.Find(userid);
                
                var siblingGroup = Sibling.SiblingGroup;
                Visits = siblingGroup.Visits;
            }

           
            return View(Visits.ToList());
        }
    

        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // GET: Visits/Create
        [Authorize (Roles = "Sibling")]
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name");
            var userId = User.Identity.GetUserId();
            var sibling = db.Siblings.Find(userId);
            var model = new Visit
            {
                GroupId = sibling.GroupId,
                VisitStart = DateTime.Now,
                VisitEnd = DateTime.Now.AddHours(3),
            };
            return View(model);
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize (Roles = "Sibling")]
        public ActionResult Create([Bind(Include = "Id,VisitStart,VisitEnd,DriverId")] Visit visit)
        {
            var userid = User.Identity.GetUserId();
            var sibling = db.Siblings.Find(userid);
            var IsValid = ModelState.IsValid;
            if (IsValid && visit.VisitStart.Date != visit.VisitEnd.Date)
            {
                IsValid = false;
                ModelState.AddModelError("VisitEnd", "Visit Must Start and End on The Same Day");
            }
            if (IsValid && visit.VisitStart > visit.VisitEnd)
            {
                IsValid = false;
                ModelState.AddModelError("VisitEnd", "Visit End Date Is Prior Vist STart Date");
            }
            if (IsValid)
            {
               
                var UnavailableDates = sibling.SiblingGroup.Siblings.SelectMany(S => S.UnavailableDates).Select(D => D.Busydate);
                if(UnavailableDates.Any(D => D == visit.VisitStart.Date))
                {
                    IsValid = false;
                    ModelState.AddModelError("VisitStart", "Date is Not Available");
                }

            }
            if (IsValid)
            {
                var SiblingGroupId = sibling.GroupId;
                visit.GroupId = SiblingGroupId;
                db.Visits.Add(visit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", visit.DriverId);
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", visit.GroupId);
            return View(visit);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", visit.DriverId);
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", visit.GroupId);
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupId,VisitStart,VisitEnd,DriverId")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "Name", visit.DriverId);
            ViewBag.GroupId = new SelectList(db.SiblingGroups, "Id", "Name", visit.GroupId);
            return View(visit);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visit visit = db.Visits.Find(id);
            db.Visits.Remove(visit);
            db.SaveChanges();
            return RedirectToAction("GroupVisits");
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

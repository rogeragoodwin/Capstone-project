using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using practice.Models;
using practice.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace practice.Controllers
{

    public class HomeController : Controller
    {
        private ProjectExampleEntities db = new ProjectExampleEntities();
        public string EventFeed(CalendarParameters model)
        {
            var userid = User.Identity.GetUserId();
            var sibling = db.Siblings.Find(userid);
            var startdate = DateTime.Parse("1970-01-01").AddMilliseconds(model.from);
            var enddate = DateTime.Parse("1970-01-01").AddMilliseconds(model.to);
            var UnavailableDates = sibling.SiblingGroup.Siblings.SelectMany(S => S.UnavailableDates).Where(u => u.Busydate >= startdate && u.Busydate <= enddate);
            var events = UnavailableDates.AsEnumerable().Select(u => new CalendarEvent(u));
            var maxbusyid = events.Any() ? events.Max(u => u.id):0;

            SiblingGroup siblingGroup;



            siblingGroup = sibling.SiblingGroup;



            var visits = siblingGroup.Visits.Where(v => v.VisitStart >= startdate && v.VisitStart <= enddate);
            var visitEvents = visits.AsEnumerable().Select(v => new CalendarEvent(v, maxbusyid * 1000));
            events = events.Concat(visitEvents);




            var apiresult = new CalendarAPIresult
            {
                success = 1,
                result = events.ToList()
            };
            var resultjson = JsonConvert.SerializeObject(apiresult);

            return resultjson;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Calendar()
        {
            return View();
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
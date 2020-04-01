using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practice.Models.APIModels
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {

        }
        public CalendarEvent(UnavailableDate unavailableDate)
        {
            id = unavailableDate.Id;
            url = "";
            title = unavailableDate.Sibling.FirstName;
            @class = "event-warning";
            start = (long) (unavailableDate.Busydate.AddHours(5) - DateTime.Parse("1970-01-01")).TotalMilliseconds;
            end = (long)(unavailableDate.Busydate.AddDays(1).AddMinutes(-1) - DateTime.Parse("1970-01-01")).TotalMilliseconds;
        }
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        public string @class { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public CalendarEvent(Visit visit,int idoffset)

        {

            id = visit.Id + idoffset;
            url = "";
            title = "Visit";
            @class = "event-success";
            start = (long)(visit.VisitStart.AddHours(5) - DateTime.Parse("1970-01-01")).TotalMilliseconds;
            var enddatetime = (visit.VisitEnd - visit.VisitStart >= TimeSpan.FromHours(3))? visit.VisitEnd: (visit.VisitStart.AddHours(3));
            end = (long)(enddatetime.AddHours(5) - DateTime.Parse("1970-01-01")).TotalMilliseconds;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practice.Models.APIModels
{
    public class CalendarAPIresult
    {
        public int success { get; set;}
        public IEnumerable<CalendarEvent> result { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practice.Models.APIModels
{
    public class CalendarParameters
    {
        public long from { get; set; }
        public long to { get; set; }
        public int? utc_offset_from { get; set; }
        public int? utc_offset_to { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace practice.Models
{
    [MetadataType(typeof(UnavailableDateAnnotations))]
    public partial class UnavailableDate
    {

    }
    public class UnavailableDateAnnotations
    {
        [DataType(DataType.Date)]
        public System.DateTime Busydate
        {
            get; set;
        }
    }
}
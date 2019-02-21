using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class RatingViewModel
    {
        public int id { get; set; }
        [DisplayName("Parcel Number")]
        public string ParcelNo { get; set; }
        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }
        public string Area { get; set; }
        public int Total { get; set; }
    }
}
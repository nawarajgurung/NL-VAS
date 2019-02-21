using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class LandUsePViewModel
    {
        public int id { get; set; }
        public string ParcelNo { get; set; }
        public string LandType { get; set; }
        public decimal Total { get; set; }
        public List<LandUseViewModel> Landuse { get; set; }
    }
}
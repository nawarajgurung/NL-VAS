using NL_VAS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class ReportsViewModel
    {
        public GeneralInfoParcel GeneralInfoParcel { get; set; }
        public List<DataentryViewModel> DataEntry { get; set; }
        public List<LandUseViewModel> LandUse { get; set; }
        public List<FactorViewModel> Factor { get; set; }
    }
}
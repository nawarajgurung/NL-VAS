using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class ConsiderationListViewModel
    {
        public int FactorValueId { get; set; }
        public bool Status { get; set; }
        public decimal? Value { get; set; }
    }
}
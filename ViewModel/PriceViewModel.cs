using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class PriceViewModel
    {
        public string ParacelNo { get; set; }
        public string Name { get; set; }
        public string MapSheetNo { get; set; }
        public decimal NetScore { get; set; }
        public decimal TotalPrice { get; set; }
        public string BaseId { get; set; }
        public decimal PerSquaremeterPrice { get; set; }
        public decimal PerUnitPrice { get; set; }
        public decimal PerUnitSquareMeterPrice { get; set; }
    }
}
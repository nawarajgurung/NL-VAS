using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class PriceSetting
    {
        public int id { get; set; }
        [ForeignKey(nameof(GeneralInfoParcel))]
        [Index(IsUnique = true)]
        public int generalparcelID { get; set;}
        public decimal perunitprice { get; set; }
        public string ParcelNo { get; set; }
        public decimal AskedPrice { get; set; }
        public GeneralInfoParcel GeneralInfoParcel { get; set; }
    }
}
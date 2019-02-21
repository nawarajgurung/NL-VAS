using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandTypeSetting
    {
        public int id { get; set; }
        [ForeignKey(nameof(LandSetting))]
        public int LandsettingId { get; set; }
        [ForeignKey(nameof(LandType))]
        public int LandTypeId { get; set; }
        [Display(Name ="Value")]
        public decimal value { get; set; }
        public LandType LandType { get; set; }
        public LandSetting LandSetting { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandUseValue
    {
        public int id { get; set; }
        [Required]
        public decimal value { get; set; }
        [ForeignKey(nameof(LandUse))]
        public int LandUseId { get; set; }
        [ForeignKey(nameof(LandSetting))]
        public int LandSettingId { get; set; }
        public LandSetting LandSetting { get; set; }
        public LandUse LandUse { get; set; }
    }
}
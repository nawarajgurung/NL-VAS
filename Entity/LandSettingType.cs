using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandSettingType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }

        [ForeignKey(nameof(LandSetting))]
        [Display(Name="Land Setting")]
        public int LandSettingID { get; set; }
        public virtual LandSetting LandSetting { get; set; }
        public virtual List<LandSettingTypeValue> LandSettingValue { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandSetting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<LandSettingType> LandSettingType { get; set; }
    }
}
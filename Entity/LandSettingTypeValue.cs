using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandSettingTypeValue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
        [ForeignKey(nameof(LandSettingType))]
        [Display(Name="Land Setting Type")]
        public int LandSettingTypeId { get; set; }
        public virtual LandSettingType LandSettingType { get; set; }
    }
}
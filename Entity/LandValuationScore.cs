using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandValuationScore
    {
        [Key]
        public int Id { get; set; }       
        [ForeignKey(nameof(LandSettingTypeValue))]
        public int landsettingvalueID { get; set; }      
        [ForeignKey(nameof(Generalinfoparcel))]
        [Display(Name ="General Info Parcel")]
        public int GeneralinfoparcelID { get; set; }
        public LandSettingTypeValue LandSettingTypeValue { get; set; }
        public GeneralInfoParcel Generalinfoparcel { get; set; }
    }
}
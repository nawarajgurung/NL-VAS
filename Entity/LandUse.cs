using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class LandUse
    {
        public int id { get; set; }
        [ForeignKey(nameof(GeneralInfoParcel))]
        public int GeneralInfoParcelId { get; set; }
        [ForeignKey(nameof(LandType))]
        [Display(Name="Land Type")]
        public int LandTypeId { get; set; }
        public LandType LandType { get; set; }
        public GeneralInfoParcel GeneralInfoParcel { get; set; }

    }
}
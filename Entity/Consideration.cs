using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class Consideration
    {
        public int Id { get; set; }
        [ForeignKey(nameof(GeneralInfoParcel))]
        [Display(Name ="General Parcel")]
        public int GeneralParcelId { get; set; }
        [ForeignKey(nameof(FactorValue))]
        [Display(Name ="Factory Value")]
        public int FactorValueId { get; set; }
        public bool Status { get; set; }
        [Required]
        public decimal Value { get; set; }
        public GeneralInfoParcel GeneralInfoParcel { get; set; }
        public virtual FactorValue FactorValue { get; set; }
    }
}
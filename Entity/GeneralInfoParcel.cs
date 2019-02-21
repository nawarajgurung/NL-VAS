using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class GeneralInfoParcel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Parcel Number")]
        public string ParcelNo { get; set; }
        [Required]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        public string Ropani { get; set; }
        public string Aana { get; set; }
        public string Daam { get; set; }
        public string Paisa { get; set; }
        [Required]
        [Display(Name ="Map Sheet Number")]
        public string MapsheetNo { get; set; }
        [Required]
        public string District { get; set; }
        [Display(Name ="VDC/Municipality")]
        [Required]
        public string VDCMunicipality { get; set; }
        [Required]
        public string Ward { get; set; }
       
        public decimal sqmeter { get; set; }
        
        public decimal TotalArea { get; set; }
    }
}
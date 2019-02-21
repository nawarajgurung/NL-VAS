using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class GeneralInfoParcelViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Parcel Number")]
        public string ParcelNo { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }
        public string Ropani { get; set; }
        public string Aana { get; set; }
        public string Daam { get; set; }
        public string Paisa { get; set; }
        [DisplayName("Map Sheet Number")]
        public string MapsheetNo { get; set; }
        public string District { get; set; }
        [Display(Name ="VDC/Municipality")]
        public string VDCMunicipality { get; set; }
        public string Ward { get; set; }
        [Display(Name="Square Meter")]
        public decimal sqmeter { get; set; }
        [Display(Name ="Total Area")]
        public decimal TotalArea { get; set; }
        public List<string> Types { get; set; }
    }
}
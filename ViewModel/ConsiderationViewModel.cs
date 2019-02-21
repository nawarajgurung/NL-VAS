using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NL_VAS.ViewModel
{
    public class ConsiderationViewModel
    {
        public int GeneralParcelId { get; set; }
        public List<ConsiderationListViewModel> considerationList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class Factor
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<FactorValue> FactorValue { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NL_VAS.Entity
{
    public class FactorValue
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Factor))]
        public int FactorId { get; set; }
        public Factor Factor { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Attributes;

namespace WooliesXTechnicalChallenge.Models
{
    public class Special
    {
        public IEnumerable<Quantity> Quantities { get; set; } = new List<Quantity>();

        public decimal Total { get; set; }
    }
}

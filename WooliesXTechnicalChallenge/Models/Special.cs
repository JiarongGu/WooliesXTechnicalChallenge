using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class Special
    {
        public IEnumerable<Quantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}

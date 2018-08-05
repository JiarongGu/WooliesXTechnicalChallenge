using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Attributes;

namespace WooliesXTechnicalChallenge.Models
{
    public class Special
    {
        [NotEmpty]
        public IEnumerable<Quantity> Quantities { get; set; }

        public decimal Total { get; set; }

        public decimal UnitPrice => Total / (Quantity == 0 ? 1 : Quantity);

        public int Quantity => Quantities?.FirstOrDefault()?.Value ?? 0;
    }
}

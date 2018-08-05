using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class TrolleyCalculatorRequest
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Special> Specials { get; set; }

        public IEnumerable<Quantity> Quantities { get; set; }
    }
}

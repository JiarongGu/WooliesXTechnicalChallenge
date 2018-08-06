using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Attributes;

namespace WooliesXTechnicalChallenge.Models
{
    public class Trolley
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<Special> Specials { get; set; } = new List<Special>();

        public IEnumerable<Quantity> Quantities { get; set; } = new List<Quantity>();
    }
}

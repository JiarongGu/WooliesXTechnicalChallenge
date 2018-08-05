using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class Product
    {
        public Product() { }

        public Product(string name, decimal price, decimal quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}

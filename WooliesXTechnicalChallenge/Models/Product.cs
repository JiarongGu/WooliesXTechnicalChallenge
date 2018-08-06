using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class Product: ICloneable
    {
        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product(string name, decimal price, decimal quantity)
            :this(name, price)
        {
            Quantity = quantity;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public decimal Quantity { get; set; }

        public object Clone()
        {
            return new Product(Name, Price, Quantity);
        }
    }
}

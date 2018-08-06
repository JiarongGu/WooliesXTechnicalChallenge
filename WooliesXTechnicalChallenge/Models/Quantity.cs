using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class Quantity
    {
        public Quantity() { }

        public Quantity(string name, int quantity)
        {
            Name = name;
            Value = quantity;
        }

        [Required]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        [Required]
        public int Value { get; set; }
    }
}

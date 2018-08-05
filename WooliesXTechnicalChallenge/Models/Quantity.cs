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
        [Required]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }
    }
}

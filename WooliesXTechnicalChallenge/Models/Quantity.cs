using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Models
{
    public class Quantity
    {
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Value { get; set; }
    }
}

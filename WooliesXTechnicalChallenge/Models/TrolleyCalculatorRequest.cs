using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Attributes;

namespace WooliesXTechnicalChallenge.Models
{
    public class TrolleyCalculatorRequest: IValidatableObject
    {
        [NotEmpty]
        public IEnumerable<Product> Products { get; set; }

        [Required]
        public IEnumerable<Special> Specials { get; set; }

        [NotEmpty]
        public IEnumerable<Quantity> Quantities { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var productName = Products.FirstOrDefault()?.Name;
            var isSpecialsVaild = !Specials.Any(x => x.Quantities.Any(y => y.Name != productName));
            var isQuantitiesVaild = !Quantities.Any(x => x.Name != productName);

            if (isSpecialsVaild && isSpecialsVaild)
            {
                yield return null;
            }
            else
            {
                yield return new ValidationResult("only accept one product at a time");
            }
        }
    }
}

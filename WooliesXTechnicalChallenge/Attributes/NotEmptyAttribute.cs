using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Attributes
{
    public class NotEmptyAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var enumerable = value as IEnumerable<object>;
            return enumerable != null && enumerable.Count() > 0;
        }
    }
}

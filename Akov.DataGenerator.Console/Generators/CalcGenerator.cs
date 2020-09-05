using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public class CalcGenerator : CalcGeneratorBase
    {
        protected override object CreateImpl(CalcPropertyObject propertyObject)
        {
            if (string.Equals(propertyObject.Property.Name, "fullname", StringComparison.OrdinalIgnoreCase))
            {
                var val1 = propertyObject.Values
                    .Single(v => String.Equals(v.Name, "firstname", StringComparison.OrdinalIgnoreCase));
                var val2 = propertyObject.Values
                    .Single(v => String.Equals(v.Name, "lastname", StringComparison.OrdinalIgnoreCase));
                return $"{val1.Value} {val2.Value}";
            }
            if(string.Equals(propertyObject.Property.Name, "count", StringComparison.OrdinalIgnoreCase))
            {
                var val1 = propertyObject.Values
                    .Single(v => String.Equals(v.Name, "students", StringComparison.OrdinalIgnoreCase))
                    .Value as List<NameValueObject>;

                return val1!.Count;
            }
            throw new NotSupportedException("Not expected calculated property");
        }

        protected override object CreateRangeFailureImpl(CalcPropertyObject propertyObject)
        {
            throw new NotSupportedException("Range failure not supported");
        }
    }
}
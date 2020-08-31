using System.Linq;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public class CalcGenerator : CalcGeneratorBase
    {
        protected override object CreateImpl(CalcPropertyObject propertyObject)
        {
            if (propertyObject.DefinitionName == "Student" && propertyObject.Property.Name == "fullname")
            {
                var val1 = propertyObject.Values.Single(v => v.Name == "firstname");
                var val2 = propertyObject.Values.Single(v => v.Name == "lastname");
                return $"{val1.Value} {val2.Value}";
            }

            throw new System.NotSupportedException("Not expected calculated property");
        }

        protected override object CreateRangeFailureImpl(CalcPropertyObject propertyObject)
        {
            throw new System.NotSupportedException("Range failure not supported");
        }
    }
}
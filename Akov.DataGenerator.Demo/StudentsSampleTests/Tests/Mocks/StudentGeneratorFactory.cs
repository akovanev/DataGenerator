using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Profiles;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks
{
    /// <summary>
    /// Extends the GeneratorFactory with the StudentCalcGenerator.
    /// </summary>
    public class StudentGeneratorFactory : GeneratorFactory
    {
        public StudentGeneratorFactory(DgProfileBase? profile = null) : base(profile)
        {
        }
        
        protected override Dictionary<string, GeneratorBase> GetGeneratorDictionary()
        {
            Dictionary<string, GeneratorBase> generators = base.GetGeneratorDictionary();
            generators.Add(TemplateType.Calc, new StudentCalcGenerator());
            return generators;
        }
    }

    /// <summary>
    /// Creates calculated values for the StudentCollection and the Student class.
    /// The logic is just an example that does not match all the best practices.
    /// </summary>
    public class StudentCalcGenerator : CalcGeneratorBase
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

            if (string.Equals(propertyObject.Property.Name, "count", StringComparison.OrdinalIgnoreCase))
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
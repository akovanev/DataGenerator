using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;
using System;

namespace Akov.DataGenerator.Generators
{
    public abstract class NumberGenerator : GeneratorBase
    {
        protected double CreateValue(
            PropertyObject propertyObject,
            double minDefault,
            double maxDefault)
        {
            Property property = propertyObject.Property;
            double min = property.MinValue != null
                ? Convert.ToDouble(property.MinValue) 
                : minDefault;
             double max = property.MaxValue != null
                ? Convert.ToDouble(property.MaxValue) 
                : maxDefault;

            return GetRandomInstance(propertyObject).GetDouble(min, max);
        }

        protected double CreateRangeFailureValue(
            PropertyObject propertyObject,
            double minDefault,
            double maxDefault)
        {
            Property property = propertyObject.Property;
            double min = property.MinValue != null
                ? Convert.ToDouble(property.MinValue)
                : minDefault;
             double max = property.MaxValue != null
                ? Convert.ToDouble(property.MaxValue)
                : maxDefault;

            double diff = max - min;
            double random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureValue))
                .GetDouble(0, diff);

            return random < diff / 2
                ? min - random - 1
                : max + random + 1;
        }
    }
}
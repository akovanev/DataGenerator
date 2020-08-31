using System.Globalization;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public class DoubleGenerator : NumberGenerator
    {
        private const double MinDefault = 0;
        private const double MaxDefault = 1;
        private const string Pattern = "0.00";

        protected override object CreateImpl(PropertyObject propertyObject)
        {
            double value = CreateValue(propertyObject, MinDefault, MaxDefault);
            return value.ToString(propertyObject.Property.Pattern ?? Pattern, CultureInfo.InvariantCulture);
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            double value = CreateRangeFailureValue(propertyObject, MinDefault, MaxDefault);
            return value.ToString(propertyObject.Property.Pattern ?? Pattern, CultureInfo.InvariantCulture);
        }
    }
}
using System.Globalization;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class DoubleGenerator : GeneratorBase
    {
        private const double MinDefault = 0;
        private const double MaxDefault = 1;
        private const string Pattern = "0.00";

        protected internal override object CreateImpl(Property property)
        {
            double min = (double?)property.MinValue ?? MinDefault;
            double max = (double?)property.MaxValue ?? MaxDefault;

            double value = GetRandomDouble(min, max);

            return value.ToString(property.Pattern ?? Pattern, CultureInfo.InvariantCulture);
        }

        protected internal override object CreateRangeFailureImpl(Property property)
        {
            double min = (double?)property.MinValue ?? MinDefault;
            double max = (double?)property.MaxValue ?? MaxDefault;

            double value = GetRandom(0, 1) == 0
                ? GetRandomDouble(-2 * min, min - 1)
                : GetRandomDouble(max + 1, 2 * max);

            return value.ToString(property.Pattern ?? Pattern, CultureInfo.InvariantCulture);
        }
    }
}
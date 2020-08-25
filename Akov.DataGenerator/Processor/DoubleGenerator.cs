using System.Globalization;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processor
{
    internal class DoubleGenerator : GeneratorBase
    {
        private const double MinDefault = 0;
        private const double MaxDefault = 1;
        private const string Pattern = "0.00";

        protected internal override object CreateImpl(Property property, Template template, int index)
        {
            double min = (double?)property.MinValue ?? MinDefault;
            double max = (double?)property.MaxValue ?? MaxDefault;

            double value = GetRandomDouble(min, max);

           return value.ToString(template.Pattern ?? Pattern, CultureInfo.InvariantCulture);
        }
    }
}
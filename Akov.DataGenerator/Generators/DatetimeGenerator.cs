using System;
using System.Globalization;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class DatetimeGenerator : GeneratorBase
    {
        private const string DefaultDateFormat = "yyyy-MM-dd";
        private readonly DateTime _minDefault = DateTime.Today.AddYears(-1);
        private readonly DateTime _maxDefault = DateTime.Today;

        protected internal override object CreateImpl(Property property, Template template)
        {
            string format = template.Pattern ?? DefaultDateFormat;

            DateTime min = property.MinValue is null 
                ? _minDefault 
                : DateTime.ParseExact((string)property.MinValue, format, CultureInfo.InvariantCulture);

            DateTime max = property.MaxValue is null
                ? _maxDefault
                : DateTime.ParseExact((string)property.MaxValue, format, CultureInfo.InvariantCulture);

            int days = (max - min).Days;

            int random = GetRandom(0, days);

            DateTime value = min.AddDays(random);

            return value.ToString(format, CultureInfo.InvariantCulture);
        }

        protected internal override object CreateRangeFailureImpl(Property property, Template template)
        {
            string format = template.Pattern ?? DefaultDateFormat;

            DateTime min = property.MinValue is null
                ? _minDefault
                : DateTime.ParseExact((string)property.MinValue, format, CultureInfo.InvariantCulture);

            DateTime max = property.MaxValue is null
                ? _maxDefault
                : DateTime.ParseExact((string)property.MaxValue, format, CultureInfo.InvariantCulture);

            DateTime value = GetRandom(0, 1) == 0
                ? min.AddDays(- GetRandom(1, 100))
                : max.AddDays(GetRandom(1, 100));

            return value.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
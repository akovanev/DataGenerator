using System;
using System.Globalization;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators;

public class DatetimeGenerator : GeneratorBase
{
    private const string DefaultDateFormat = "yyyy-MM-dd";
    private readonly DateTime _minDefault = DateTime.Today.AddYears(-1);
    private readonly DateTime _maxDefault = DateTime.Today;

    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Func<DateTime, int, DateTime> getDateTime = (min, days) =>
        {
            int random = GetRandomInstance(propertyObject).GetInt(0, days);
            return min.AddDays(random);
        };

        return CreateImpl(propertyObject, getDateTime);
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        Func<DateTime, int, DateTime> getDateTime = (min, days) =>
        {
            int random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl)).GetInt(0, days);
            return random < days / 2
                ? min.AddDays(-random - 1)
                : min.AddDays(days + 1 + random);
        };

        return CreateImpl(propertyObject, getDateTime);
    }

    private object CreateImpl(PropertyObject propertyObject, Func<DateTime, int, DateTime> getDateTime)
    {
        Property property = propertyObject.Property;
        string format = property.Pattern ?? DefaultDateFormat;

        DateTime min = property.MinValue is null 
            ? _minDefault 
            : DateTime.ParseExact((string)property.MinValue, format, CultureInfo.InvariantCulture);

        DateTime max = property.MaxValue is null
            ? _maxDefault
            : DateTime.ParseExact((string)property.MaxValue, format, CultureInfo.InvariantCulture);

        int days = (max - min).Days;

        DateTime value = getDateTime(min, days);

        return value.ToString(format, CultureInfo.InvariantCulture);
    }
}
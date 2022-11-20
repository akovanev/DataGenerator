using System;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;

public class UIntGenerator : NumberGenerator
{
    private const double MinDefault = 0;
    private const double MaxDefault = 100;
    
    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;

        if (Convert.ToInt32(property.MinValue) < 0)
            throw new ArgumentException($"Min value for {nameof(UIntGenerator)} cannot be less than zero");
        
        double min = property.MinValue is not null
            ? Convert.ToDouble(property.MinValue) 
            : MinDefault;
        double max = property.MaxValue is not null
            ? Convert.ToDouble(property.MaxValue) 
            : MaxDefault;

        return (int)GetRandomInstance(propertyObject).GetDouble(min, max);
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));
        return random.GetInt(-100, -1);
    }
}
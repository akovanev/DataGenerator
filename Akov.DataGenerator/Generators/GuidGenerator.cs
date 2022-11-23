using System;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class GuidGenerator : GeneratorBase
{
    private const string Pattern = "D";

    protected override object CreateImpl(PropertyObject propertyObject)
    {
        return Guid.NewGuid().ToString(propertyObject.Property.Pattern ?? Pattern);
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        return Guid.Empty;
    }
}
using System;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class DecimalGenerator : DoubleGenerator
{
    protected override object CreateImpl(PropertyObject propertyObject)
        => Convert.ToDecimal(base.CreateImpl(propertyObject));

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        => Convert.ToDecimal(base.CreateRangeFailureImpl(propertyObject));
}
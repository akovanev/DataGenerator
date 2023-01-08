using System;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

// ReSharper disable once InconsistentNaming
public class DefaultTypeValueGenerator : GeneratorBase
{
    protected override object CreateImpl(PropertyObject propertyObject)
        => propertyObject.Property.Type switch
        {
            TemplateType.String => "string",
            TemplateType.Bool => false,
            TemplateType.Set => 0,
            TemplateType.Int => 0,
            TemplateType.Double => 0.00,
            TemplateType.Decimal => 0.00,
            TemplateType.Guid => Guid.Empty,
            TemplateType.DateTime => new DateTime(2023, 1, 1),
            TemplateType.Phone => "+1 222 333 444",
            TemplateType.Email => "mail@example.com",
            TemplateType.IpV4 => "0.0.0.0",
            _ => null!
        };

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        throw new NotSupportedException($"{nameof(DefaultTypeValueGenerator)} can not response with failures");
    }
}
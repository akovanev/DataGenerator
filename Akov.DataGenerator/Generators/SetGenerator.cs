﻿using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators;

public class SetGenerator : GeneratorBase
{
    private const string DefaultPattern = "00000";

    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;

        string? pattern = property.Type! == TemplateType.File ||
                          property.Type == TemplateType.Resource
            ? propertyObject.PredefinedValues as string
            : property.Pattern;
            
        pattern ??= DefaultPattern;
        property.SequenceSeparator ??= ",";

        var (count, _) = pattern.GetSplitSizeOrString(property.SequenceSeparator);
        
        int random = GetRandomInstance(propertyObject).GetInt(0, count);

        var (_, result) = pattern.GetSplitSizeOrString(property.SequenceSeparator, random);

        return result;
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        return DefaultPattern;
    }
}
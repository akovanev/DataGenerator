using System;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Factories;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Extensions;

internal static class RandomFactoryExtensions
{
    private const int DefaultArrayMinCount = 0;
    private const int DefaultArrayMaxCount = 5;

    public static int GetArraySize(this RandomFactory factory, PropertyObject propertyObject)
    {
        if(propertyObject.Property.Type != TemplateType.Array)
            throw new ArgumentException($"Property is of type {propertyObject.Property.Type} but {TemplateType.Array} expected");

        int minCount = propertyObject.Property.MinLength ?? DefaultArrayMinCount;
        int maxCount = propertyObject.Property.MaxLength ?? DefaultArrayMaxCount;

        Random random = factory.GetOrCreate(
            propertyObject.DefinitionName, 
            propertyObject.Property.Name!, 
            nameof(GetArraySize));

        return random.GetInt(minCount, maxCount);
    }
}
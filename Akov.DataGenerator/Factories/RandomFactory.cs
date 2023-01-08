using System;
using System.Collections.Concurrent;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Factories;

internal class RandomFactory
{
    private const int DefaultArrayMinCount = 0;
    private const int DefaultArrayMaxCount = 5;
    private readonly ConcurrentDictionary<string, Random> _randoms = new();

    public Random GetOrCreate(string definitionName, string propertyName, string step)
    {
        string key = $"{definitionName}.{propertyName}_{step}";
        _randoms.TryAdd(key, ThreadSafeRandom.Instance);
        return _randoms[key];
    }

    public virtual int GetArraySize(PropertyObject propertyObject)
    {
        if(propertyObject.Property.Type != TemplateType.Array)
            throw new ArgumentException($"Property is of type {propertyObject.Property.Type} but {TemplateType.Array} expected");

        int minCount = propertyObject.Property.MinLength ?? DefaultArrayMinCount;
        int maxCount = propertyObject.Property.MaxLength ?? DefaultArrayMaxCount;

        Random random = GetOrCreate(
            propertyObject.DefinitionName, 
            propertyObject.Property.Name!, 
            nameof(GetArraySize));

        return random.GetInt(minCount, maxCount);
    }
}
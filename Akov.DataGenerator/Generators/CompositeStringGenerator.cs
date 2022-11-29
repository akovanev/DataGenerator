using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators;

public class CompositeStringGenerator : StringGenerator
{
    private const string CompositeRegex = @"(\\[\{\}])+|([^{}]+)";
    private static readonly ConcurrentDictionary<string, Dictionary<string, Tuple<int, int>>> PatternList = new();
    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;
        property.Pattern.ThrowIfNull("Pattern should not be null");

        if (!PatternList.TryGetValue(property.Pattern!, out var patternList))
        {
            patternList = GetPatternList(property.Pattern!);
            PatternList.TryAdd(property.Pattern!, patternList);
        }
        
        Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));
        var value = new StringBuilder();
        foreach (var pattern in patternList)
        {
            int length = random.GetInt(pattern.Value.Item1, pattern.Value.Item2);
            value.Append(CreateString(propertyObject, pattern.Key, length, 0));
        }
        
        return value.ToString();
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        throw new NotImplementedException();
    }

    private Dictionary<string, Tuple<int, int>> GetPatternList(string pattern)
    {
        Dictionary<string, Tuple<int, int>> patternList = new();
        var regex = new Regex(CompositeRegex);
        var matches = regex.Matches(pattern!);

        if (matches.Count % 2 != 0)
            throw new InvalidOperationException("Range(s) is not defined properly for the composite pattern");
        
        for (int i = 0; i < matches.Count; i+=2)
        {
            var key = matches[i].Groups[0].ToString()
                .Replace(@"\{", "{")
                .Replace(@"\}", "}");
            var range = matches[i+1].Groups[0].ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);
            int min = Convert.ToInt32(range[0]);
            int max = range.Length > 1 ? Convert.ToInt32(range[1]) : min;
            patternList.Add(key, new Tuple<int, int>(min, max));
        }

        return patternList;
    }
}
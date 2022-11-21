using System;
using System.Linq;
using System.Text;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators;

public class PhoneGenerator : GeneratorBase
{
    private const char SpecialSymbol = '#';
    private const char SeparatorSymbol = ';';
    private const string DefaultPattern = "+## ### ### ###";

    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;
        
        string[] patterns = string.IsNullOrWhiteSpace(property.Pattern)
            ? new []{ DefaultPattern }
            : property.Pattern.Split(SeparatorSymbol);
        
        Random random = GetRandomInstance(propertyObject);

        string pattern = patterns.Length > 1
            ? patterns[random.GetInt(0, patterns.Length - 1)]
            : patterns[0];

        var builder = new StringBuilder();
        
        foreach (var c in pattern)
            builder.Append(c != SpecialSymbol ? c : random.GetInt(0, 9).ToString());

        return builder.ToString();
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;
        string[] patterns = string.IsNullOrWhiteSpace(property.Pattern)
            ? new []{ DefaultPattern }
            : property.Pattern.Split(SeparatorSymbol);
        
        int minLength = patterns.Min(p => p.Length);
        int maxLength = patterns.Max(p => p.Length);
            
        Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));

        int length = minLength > 1 && random.GetInt(0, 1) == 0
            ? random.GetInt(0, minLength - 1)
            : random.GetInt(maxLength + 1, maxLength * 2);
        
        var builder = new StringBuilder();
        var pattern = patterns.First();
        for (int i = 0; i < length; i++)
            builder.Append(i < pattern.Length && pattern[i] != SpecialSymbol 
                ? pattern[i] 
                : random.GetInt(0, 9));

        return builder.ToString();
    }
}
using Akov.DataGenerator.Constants;
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

        var (count, _) = GetSplitSizeOrString(pattern, property.SequenceSeparator);
        
        int random = GetRandomInstance(propertyObject).GetInt(0, count);

        var (_, result) = GetSplitSizeOrString(pattern, property.SequenceSeparator, random);

        return result;
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        return DefaultPattern;
    }
    
    private static (int, string) GetSplitSizeOrString(string source, string separator, int substring = -1)
    {
        int count = 0;
        int prev = 0;
        int i;

        for (i = 0; i < source.Length; i++)
        {
            if (source[i] != separator[0]) continue;

            bool isMatch = true;

            for (int j = 1; j < separator.Length; j++)
            {
                if (source[i + j] == separator[j]) continue;
                
                isMatch = false;
                break;
            }

            if (!isMatch) continue;

            if (count == substring)
                return (count + 1, source.Substring(prev, i - prev));

            if (i + separator.Length == source.Length) break;
            
            prev = i + separator.Length;
            i = prev - 1;
            count++;
        }

        return (count + 1, source[prev..]);
    }
}
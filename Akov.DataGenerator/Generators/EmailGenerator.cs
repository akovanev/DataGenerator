using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class EmailGenerator : GeneratorBase
{
    //Todo: should be redesigned
    private static readonly ResourceReader ResourceReader = new();

    protected override object CreateImpl(PropertyObject propertyObject)
    {
        var builder = new StringBuilder();
        Random random = GetRandomInstance(propertyObject);
        bool hasFirstName = random.Next(5) > 2;
        bool hasLastName = !hasFirstName || random.Next(5) > 1;
        bool hasEndNumbers = random.Next(5) > 2;
        
        if (hasFirstName)
        {
            builder.Append(GetValueFromSet(ResourceType.FirstNames));
            if (hasLastName)
            {
                int separatorProbability = random.Next(4);
                switch (separatorProbability)
                {
                    case 3:
                    case 2:
                        builder.Append('.');
                        break;
                    case 1:
                        builder.Append('-');
                        break;
                }
            }
        }

        if (hasLastName)
            builder.Append(GetValueFromSet(ResourceType.LastNames));

        if (hasEndNumbers)
            builder.Append(random.Next(2050));

        builder.Append('@');
        builder.Append(GetValueFromSet(ResourceType.EmailDomains));
        
        return builder.ToString();
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        throw new NotSupportedException("Range failure is not supported for emails in the current version.");
    }

    private string GetValueFromSet(string resource)
    {
        var values = ResourceReader.ReadEmbeddedResource(resource)!;
        var (count, _) = GetSplitSizeOrString(values, ",");
        int random = new Random().Next(count);
        return GetSplitSizeOrString(values, ",", random).Item2;
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
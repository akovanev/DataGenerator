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
    private static readonly ConcurrentDictionary<string, string[]> Resources = new();

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
        Resources.TryAdd(resource, ResourceReader.ReadEmbeddedResource(resource)!.Split(","));
        var values = Resources[resource];
        int random = new Random().Next(values.Length);
        return values[random];
    }
}
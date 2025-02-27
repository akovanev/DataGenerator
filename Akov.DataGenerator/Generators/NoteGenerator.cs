using System;
using System.Text;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class NoteGenerator : StringGenerator
{
    private static readonly ResourceReader ResourceReader = new();
    
    protected override string CreateString(PropertyObject propertyObject, string pattern, int length, int spaces)
    {
        Random randomWord = GetRandomInstance(propertyObject, nameof(randomWord));
        var intGenerator = new IntGenerator();
        var builder = new StringBuilder();

        while (spaces >= 0)
        {
            var wordTypeIndex = randomWord.GetInt(0, 2);

            switch (wordTypeIndex)
            {
                case 0:
                    builder.Append(GetValueFromSet(ResourceType.Verbs));
                    break;
                case 1:
                    builder.Append(GetValueFromSet(ResourceType.Nouns));
                    break;
                default:
                    builder.Append(intGenerator.Create(propertyObject));
                    break;
            }

            spaces--;
        }

        return builder.ToString()[1..];
    }
    
    private string GetValueFromSet(string resource)
    {
        var values = ResourceReader.ReadEmbeddedResource(resource)!;
        var (count, _) = values.GetSplitSizeOrString(",");
        int random = new Random().Next(count);
        return values.GetSplitSizeOrString(",", random).Item2;
    }
}
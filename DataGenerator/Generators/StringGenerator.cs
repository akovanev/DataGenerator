using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;
using Akov.DataGenerator.Utilities;

namespace Akov.DataGenerator.Generators;

public class StringGenerator : GeneratorBase<string>
{
    public override string CreateRandomValue(Property property)
    {
        var minLength = property.GetValueRule(ValueRules.MinLength) ?? DefaultValues.Values[ValueRules.MinLength];
        var maxLength = property.GetValueRule(ValueRules.MaxLength) ?? DefaultValues.Values[ValueRules.MaxLength];
        var random = GetRandomInstance(property);

        var template = property.GetValueRule(ValueRules.Template);

        return template is null 
            ? GenerateRandomString(random, (int)minLength, (int)maxLength)
            : TemplateProcessor.CreateValue(random, template.ToString()!);
    }

    private static string GenerateRandomString(Random random, int minLength, int maxLength)
    {
        int length = random.Next(minLength, maxLength + 1); // Random length between min and max
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}

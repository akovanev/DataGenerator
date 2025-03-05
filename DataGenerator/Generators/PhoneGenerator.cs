using System.Text;
using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public class PhoneGenerator : GeneratorBase<string>
{
    private const char SpecialSymbol = '#';

    public override string CreateRandomValue(Property property)
    {
        var mask = property.GetValueRule(ValueRules.PhoneMask) ?? DefaultValues.Values[ValueRules.PhoneMask];

        var builder = new StringBuilder();
        Random random = GetRandomInstance(property);

        foreach (var c in mask.ToString()!)
            builder.Append(c != SpecialSymbol ? c : random.Next(0, 10).ToString());

        return builder.ToString();
    }
}
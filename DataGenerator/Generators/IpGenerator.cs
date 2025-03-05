using System.Text;
using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

public class IpGenerator : GeneratorBase<string>
{
    public override string CreateRandomValue(Property property)
    {
        var builder = new StringBuilder();
        Random random = GetRandomInstance(property);

        builder.Append(random.Next(1, 256));
        builder.Append('.');
        builder.Append(random.Next(1, 256));
        builder.Append('.');
        builder.Append(random.Next(1, 256));
        builder.Append('.');
        builder.Append(random.Next(1, 256));

        return builder.ToString();
    }
}
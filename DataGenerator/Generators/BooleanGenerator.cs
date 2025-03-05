using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

public class BooleanGenerator : GeneratorBase
{
    public override object CreateBoxedRandomValue(Property property)
        => Convert.ToBoolean(GetRandomInstance(property).Next(0,2));
}
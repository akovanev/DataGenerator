using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

public class GuidGenerator : GeneratorBase<Guid>
{
    public override Guid CreateRandomValue(Property property)
        => Guid.NewGuid();
}
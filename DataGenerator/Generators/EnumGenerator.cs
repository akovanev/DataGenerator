using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

public class EnumGenerator : GeneratorBase<Enum>
{
    public override Enum? CreateRandomValue(Property property)
    {
        if (!property.Type.IsEnum)
            throw new ArgumentException($"Property {property.Name} must be an enum");

        var random = GetRandomInstance(property);
        return (Enum?)Enum
            .GetValues(property.Type)
            .GetValue(random.Next(Enum.GetValues(property.Type).Length));
    }
}
using System.Collections;
using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public class SetGenerator : GeneratorBase<object>
{
    public override object? CreateRandomValue(Property property)
    {
        var value = property.GetValueRule(ValueRules.Set)
                       ?? throw new InvalidOperationException($"The array/list of values is not setup for the property {property.Name}.");
        
        var array = ((IEnumerable)value).Cast<object?>().ToArray();

        var random = GetRandomInstance(property);
        return array[random.Next(array.Length)];
    }
}
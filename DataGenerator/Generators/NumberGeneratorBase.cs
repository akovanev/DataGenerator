using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public abstract class NumberGeneratorBase<T> : GeneratorBase<T>
{
    public override T CreateRandomValue(Property property)
    {
        var minValue = property.GetValueRule(ValueRules.MinValue) ?? DefaultValues.Values[ValueRules.MinValue];
        var maxValue = property.GetValueRule(ValueRules.MaxValue) ?? DefaultValues.Values[ValueRules.MinValue];
        var random = GetRandomInstance(property);
        return CreateRandomValue(random, minValue, maxValue);
    }
    
    protected abstract T CreateRandomValue(Random random, object minValue, object maxValue);
}
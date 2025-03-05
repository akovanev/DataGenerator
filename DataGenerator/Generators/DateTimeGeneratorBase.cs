using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public abstract class DateTimeGeneratorBase<T> : GeneratorBase<T>
{
    public override T CreateRandomValue(Property property)
    {
        var min = Convert.ToDateTime(property.GetValueRule(ValueRules.MinDateValue) ??
                                     DefaultValues.Values[ValueRules.MinDateValue]);
        var max = Convert.ToDateTime(property.GetValueRule(ValueRules.MaxDateValue) ??
                                     DefaultValues.Values[ValueRules.MaxDateValue]);
        var random = GetRandomInstance(property);

        long range = (max - min).Ticks;
        long randomTicks = (long)(random.NextDouble() * range);
        return CreateRandomValue(min, randomTicks);
    }
    
    protected abstract T CreateRandomValue(DateTimeOffset dateTimeOffset, long ticks);
}
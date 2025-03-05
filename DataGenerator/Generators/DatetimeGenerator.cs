using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public class DatetimeGenerator : GeneratorBase<DateTime>
{
    public override DateTime CreateRandomValue(Property property)
    {
        var min = Convert.ToDateTime(property.GetValueRule(ValueRules.MinDateValue) ?? DefaultValues.Values[ValueRules.MinDateValue]);
        var max = Convert.ToDateTime(property.GetValueRule(ValueRules.MaxDateValue) ?? DefaultValues.Values[ValueRules.MaxDateValue]);
        var random = GetRandomInstance(property);

        long range = (max - min).Ticks;
        long randomTicks = (long)(random.NextDouble() * range);
        return min.AddTicks(randomTicks);
    }
}
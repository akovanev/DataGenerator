namespace Akov.DataGenerator.Generators;

public class DateTimeOffsetGenerator : DateTimeGeneratorBase<DateTimeOffset>
{
    protected override DateTimeOffset CreateRandomValue(DateTimeOffset dateTimeOffset, long ticks)
        => dateTimeOffset.AddTicks(ticks);
}

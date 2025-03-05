namespace Akov.DataGenerator.Generators;

public class DatetimeGenerator : DateTimeGeneratorBase<DateTime>
{
    protected override DateTime CreateRandomValue(DateTimeOffset dateTimeOffset, long ticks)
        => dateTimeOffset.UtcDateTime.AddTicks(ticks);
}
namespace Akov.DataGenerator.Generators;

public class LongGenerator : NumberGeneratorBase<long>
{
    protected override long CreateRandomValue(Random random, object minValue, object maxValue)
    {
        long min = Convert.ToInt64(minValue);
        long max = Convert.ToInt64(maxValue);
        return min + (long)(random.NextDouble() * (max - min));
    }
}
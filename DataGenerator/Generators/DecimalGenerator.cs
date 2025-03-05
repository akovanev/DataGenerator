namespace Akov.DataGenerator.Generators;

public class DecimalGenerator : NumberGeneratorBase<decimal>
{
    protected override decimal CreateRandomValue(Random random, object minValue, object maxValue)
    {
        double min = Convert.ToDouble(minValue);
        double max = Convert.ToDouble(maxValue);
        return Convert.ToDecimal(min + random.NextDouble() * (max - min));
    }
}
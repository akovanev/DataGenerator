namespace Akov.DataGenerator.Generators;

public class DoubleGenerator : NumberGeneratorBase<double>
{
    protected override double CreateRandomValue(Random random, object minValue, object maxValue)
    {
        double min = Convert.ToDouble(minValue);
        double max = Convert.ToDouble(maxValue);
        return min + random.NextDouble() * (max - min);
    }
}
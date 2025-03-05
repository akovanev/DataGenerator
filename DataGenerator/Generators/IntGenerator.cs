namespace Akov.DataGenerator.Generators;

public class IntGenerator : NumberGeneratorBase<int>
{
    protected override int CreateRandomValue(Random random, object minValue, object maxValue)
        => random.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
}
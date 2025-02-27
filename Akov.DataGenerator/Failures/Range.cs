namespace Akov.DataGenerator.Failures;

internal class Range(double min, double max)
{
    public double Min { get; } = min;
    public double Max { get; } = max;

    public bool In(double value)
    {
        return Min <= value && value < Max;
    }
}
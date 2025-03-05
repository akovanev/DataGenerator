namespace Akov.DataGenerator.Core.GenerationRules;

internal class Range(double min, double max)
{
    private readonly double _min = min;
    private readonly double _max = max;

    public bool In(double value)
        => _min <= value && value < _max;
}
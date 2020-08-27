namespace Akov.DataGenerator.Failures
{
    internal class Range
    {
        internal Range(double min, double max)
        {
            Min = min;
            Max = max;
        }

        internal double Min { get; }
        internal double Max { get; }

        internal bool In(double value)
        {
            return Min <= value && value < Max;
        }
    }
}
namespace Akov.DataGenerator.Failures
{
    internal class Range
    {
        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public double Min { get; }
        public double Max { get; }

        public bool In(double value)
        {
            return Min <= value && value < Max;
        }
    }
}
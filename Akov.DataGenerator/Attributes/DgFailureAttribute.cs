using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgFailureAttribute : Attribute
    {
        public double NullProbability { get; set; }
        public double CustomProbability { get; set; }
        public double OutOfRangeProbability { get; set; }
    }
}
using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents probabilities of different failure types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgFailureAttribute : Attribute
    {
        public double NullProbability { get; set; }
        public double CustomProbability { get; set; }
        public double OutOfRangeProbability { get; set; }
    }
}
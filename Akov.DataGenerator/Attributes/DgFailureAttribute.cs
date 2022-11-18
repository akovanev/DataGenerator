using System;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents probabilities of different failure types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgFailureAttribute : Attribute
    {
        private double _nullProbability;
        public double NullProbability
        {
            get => _nullProbability;
            set
            {
                value.ThrowIfNotInRange(0,1,$"{nameof(NullProbability)}  cannot be less than 0 or more than 1");
                _nullProbability = value;
            }
        }

        private double _customProbability;
        public double CustomProbability
        {
            get => _customProbability;
            set
            {
                value.ThrowIfNotInRange(0, 1, $"{nameof(CustomProbability)}  cannot be less than 0 or more than 1");
                _customProbability = value;
            }
        }

        private double _outOfRangeProbability;
        public double OutOfRangeProbability
        {
            get => _outOfRangeProbability;
            set
            {
                value.ThrowIfNotInRange(0, 1, $"{nameof(OutOfRangeProbability)}  cannot be less than 0 or more than 1");
                _outOfRangeProbability = value;
            }
        }
    }
}
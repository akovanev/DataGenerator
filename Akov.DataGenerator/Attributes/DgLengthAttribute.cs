using System;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Specifies the length for strings and arrays.
    /// In the current version arrays support only a fixed length of the Max value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgLengthAttribute : Attribute
    {
        private int _min;
        public int Min
        {
            get => _min;
            set
            {
                value.ThrowIfNegative($"{nameof(Min)} value should not be less than 0");
                _min = value;
            }
        }

        private int _max;
        public int Max
        {
            get => _max;
            set
            {
                value.ThrowIfNegative($"{nameof(Max)} value should not be less than 0");
                _max = value;
            }
        }
    }
}
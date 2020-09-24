using System;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the number of spaces that could appear in a random string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSpacesCountAttribute : Attribute
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
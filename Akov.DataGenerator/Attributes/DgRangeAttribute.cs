using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the range of values for the property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgRangeAttribute : Attribute
    {
        public object? Min { get; set; }
        public object? Max { get; set; }
    }
}
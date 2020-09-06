using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Specifies the length for strings and arrays.
    /// In the current version arrays support only a fixed length of the Max value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgLengthAttribute : Attribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
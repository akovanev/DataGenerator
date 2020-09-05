using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgLengthAttribute : Attribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
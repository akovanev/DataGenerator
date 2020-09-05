using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgRangeAttribute : Attribute
    {
        public double? Min { get; set; }
        public double? Max { get; set; }
    }
}
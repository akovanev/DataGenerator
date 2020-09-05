using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgRangeAttribute : Attribute
    {
        public object Min { get; set; }
        public object Max { get; set; }
    }
}
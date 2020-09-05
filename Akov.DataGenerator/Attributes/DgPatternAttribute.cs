using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgPatternAttribute : Attribute
    {
        public DgPatternAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
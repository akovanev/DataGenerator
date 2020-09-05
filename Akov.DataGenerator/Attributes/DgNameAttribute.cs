using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgNameAttribute : Attribute
    {
        public DgNameAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
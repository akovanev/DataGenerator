using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgCustomFailureAttribute : Attribute
    {
        public DgCustomFailureAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
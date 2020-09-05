using System;

namespace Akov.DataGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSequenceSeparatorAttribute : Attribute
    {
        public DgSequenceSeparatorAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the name for the property in the generated data.
    /// If it is missed, then the class property name will be taken.
    /// </summary>
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
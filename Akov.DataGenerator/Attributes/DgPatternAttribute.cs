using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the pattern.
    /// Please read more on https://github.com/akovanev/DataGenerator/
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgPatternAttribute : Attribute
    {
        public DgPatternAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    /// <summary>
    /// Represents the sub pattern.
    /// Please read more on https://github.com/akovanev/DataGenerator/
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSubTypePatternAttribute : DgPatternAttribute
    {
        public DgSubTypePatternAttribute(string value) : base(value) { }
    }
}
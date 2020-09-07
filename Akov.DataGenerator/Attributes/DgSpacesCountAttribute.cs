using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the number of spaces that could appear in a random string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSpacesCountAttribute : Attribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the source for the property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSourceAttribute : Attribute
    {
        public DgSourceAttribute(string path)
        {
            Path = path;
        }
        public string Path { get; }
    }
}
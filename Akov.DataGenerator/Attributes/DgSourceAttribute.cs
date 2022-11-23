using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Represents the source for the property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgSourceAttribute : Attribute
    {
        public DgSourceAttribute(string path, bool embedded = false)
        {
            Path = path;
            Embedded = embedded;
        }
        public string Path { get; }
        
        public bool Embedded { get; }
    }
}
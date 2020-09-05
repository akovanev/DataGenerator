using System;

namespace Akov.DataGenerator.Attributes
{
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
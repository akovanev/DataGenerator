using System.Collections.Generic;

namespace Akov.DataGenerator.Scheme
{
    public class Definition
    {
        public Definition() {}

        public Definition(string name, List<Property> properties)
        {
            Name = name;
            Properties = properties;
        }

        public string? Name { get; set; }
        public List<Property>? Properties {get; set;}
    }
}
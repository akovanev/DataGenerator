using System.Collections.Generic;
using System.Linq;

namespace Akov.DataGenerator.Scheme
{
    internal class DataScheme
    {
        public int? ObjectCount { get; set; }
        public int? AttributesCount { get; set; }
        public string? OutPropertiesName { get; set; }
        public string? OutAttributesName { get; set; }
        public List<Template>? Templates { get; set; }
        public List<Property>? Properties { get; set; }
        public List<Property>? Attributes { get; set; }

        public Template GetTemplate(string templateName)
        {
            return Templates.Single(template => template.Name == templateName);
        }
    }
}

using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Models
{
    public class PropertyObject
    {
        public PropertyObject(string definitionName, Property property, List<string>? predefinedValues = null)
        {
            DefinitionName = definitionName;
            Property = property;
            PredefinedValues = predefinedValues;
        }

        public string DefinitionName { get; }
        public Property Property { get; }
        public List<string>? PredefinedValues { get; }
    }
}
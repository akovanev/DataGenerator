using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Models
{
    public class PropertyObject
    {
        public PropertyObject(string definitionName, Property property, object? predefinedValues = null)
        {
            DefinitionName = definitionName;
            Property = property;
            PredefinedValues = predefinedValues;
        }

        public string DefinitionName { get; }
        public Property Property { get; }
        public object? PredefinedValues { get; }
    }
}
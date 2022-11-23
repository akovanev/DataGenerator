using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Models;

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

    public static PropertyObject CreateWithTypeAndPattern(
        PropertyObject propertyObject,
        string newType,
        string? newPattern)
    {
        var property = propertyObject.Property.Clone();
        property.Type = newType;
        property.Pattern = newPattern;
        return new PropertyObject(
            propertyObject.DefinitionName,
            property,
            newPattern);
    }
}
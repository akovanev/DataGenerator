using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Models;

public class PropertyObject(string definitionName, Property property, object? predefinedValues = null)
{
    public string DefinitionName { get; } = definitionName;
    public Property Property { get; } = property;
    public object? PredefinedValues { get; } = predefinedValues;

    public static PropertyObject CreateWithTypeAndPattern(
        PropertyObject propertyObject,
        string newType,
        string? newPattern)
    {
        var property = propertyObject.Property with
        {
            Type = newType,
            Pattern = newPattern
        };
        return new PropertyObject(
            propertyObject.DefinitionName,
            property,
            newPattern);
    }
}
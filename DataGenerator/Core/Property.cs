using System.Linq.Expressions;
using System.Reflection;
using Akov.DataGenerator.Core.Constants;
using Akov.DataGenerator.Core.GenerationRules;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Utilities;

namespace Akov.DataGenerator.Core;

/// <summary>
/// Represents metadata about a property, including its type, nullability, and generation rules.
/// </summary>
public record Property
{
    private Property(string propertyName, Type propertyType, bool isNullable, Type parentType)
    {
        Name = propertyName;
        Type = propertyType;
        IsNullable = isNullable;
        ParentType = parentType;
        IsBulk = propertyType.IsEnumerableExceptString();
        IsObject = propertyType.IsClassExceptString();
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Gets the type of the parent class that defines this property.
    /// </summary>
    public Type ParentType { get; }

    /// <summary>
    /// Gets a value indicating whether the property is nullable.
    /// </summary>
    public bool IsNullable { get; }

    /// <summary>
    /// Gets a value indicating whether the property represents a collection (excluding strings).
    /// </summary>
    public bool IsBulk { get; }

    /// <summary>
    /// Gets a value indicating whether the property represents a complex object (excluding strings).
    /// </summary>
    public bool IsObject { get; }

    /// <summary>
    /// Gets or sets a custom generator used to generate values for this property.
    /// </summary>
    public GeneratorBase? CustomGenerator { get; set; }

    /// <summary>
    /// Gets a list of additional rules that define how values should be generated based on probability,  
    /// supplementing the main generation logic.
    /// </summary>
    public List<GenerationRule> GenerationRules { get; } = [];

    internal Expression<Func<object, object?>>? Constructor { get; set; }
    internal Func<object?, object?>? Decorator { get; set; }
    private Guid Id { get; }
    private Dictionary<string, object?> ValueRules { get; } = new(DefaultValues.Values!);

    /// <summary>
    /// Creates a <see cref="Property"/> instance from reflection metadata.
    /// </summary>
    /// <param name="propertyInfo">The reflection metadata of the property.</param>
    /// <returns>A new <see cref="Property"/> instance.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the declaring type of <paramref name="propertyInfo"/> is null.
    /// </exception>
    public static Property CreateProperty(PropertyInfo propertyInfo)
    {
        if(propertyInfo.DeclaringType is null)
            throw new ArgumentNullException(nameof(propertyInfo), $"Declaring type is null for {propertyInfo.Name}");
        
        var type = propertyInfo.PropertyType;
        bool isNullable;

        if (propertyInfo.PropertyType.IsClass)
        {
            isNullable = NullableTypeChecker.IsNullableReferenceType(propertyInfo);
        }
        else
        {
            (type, isNullable) = propertyInfo.PropertyType.GetTypeOrNullableType();
        }
        
        return new Property(propertyInfo.Name, type, isNullable, propertyInfo.DeclaringType);
    }

    /// <summary>
    /// Creates a new <see cref="Property"/> instance for an element type in a collection.
    /// </summary>
    /// <param name="property">The original property representing a collection.</param>
    /// <param name="elementType">The type of elements in the collection.</param>
    /// <returns>A new <see cref="Property"/> instance representing the element type.</returns>
    public static Property CreateElementTypeProperty(Property property, Type elementType)
    {
        var (type, _) = elementType.GetTypeOrNullableType();
        return new Property(property.Name, type, false, property.ParentType);
    }

    /// <summary>
    /// Retrieves the value of the property rule.
    /// </summary>
    /// <param name="name">The name of the rule.</param>
    /// <returns>The value of the rule if it exists; otherwise, <c>null</c>.</returns>
    public object? GetValueRule(string name)
    {
        ValueRules.TryGetValue(name, out var value);
        return value;
    }

    /// <summary>
    /// Sets the value of the property rule.
    /// </summary>
    /// <param name="name">The name of the rule.</param>
    /// <param name="value">The value to set.</param>
    public void SetValueRule(string name, object? value)
        => ValueRules[name] = value;
}
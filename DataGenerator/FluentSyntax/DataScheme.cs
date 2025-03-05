using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.FluentSyntax;

/// <summary>
/// Manages type-based configurations and property metadata for data generation.
/// </summary>
public class DataScheme
{
    private readonly Dictionary<string, TypeBuilderBase> _typeObjects = new();

    /// <summary>
    /// Retrieves or creates a <see cref="TypeBuilder{T}"/> instance for the specified type.
    /// </summary>
    /// <typeparam name="T">The type for which to retrieve or create a type builder.</typeparam>
    /// <returns>A <see cref="TypeBuilder{T}"/> instance used for configuring type-based rules.</returns>
    public TypeBuilder<T> ForType<T>()
    {
        string fullTypeName = GetFullTypeName(typeof(T));

        if (_typeObjects.TryGetValue(fullTypeName, out var existingTypeObject)) 
            return (TypeBuilder<T>)existingTypeObject;

        var newTypeObject = new TypeBuilder<T>();
        _typeObjects.Add(fullTypeName, newTypeObject);

        return newTypeObject;
    }

    internal Property? LookupFor(Type type, string propertyName)
    {
        string fullTypeName = GetFullTypeName(type);
        return _typeObjects.TryGetValue(fullTypeName, out var typeObject)
            ? typeObject.TypeProperties.GetValueOrDefault(propertyName)
            : null;
    }

    private static string GetFullTypeName(Type type)
        => type.FullName ?? throw new InvalidOperationException($"{nameof(DataScheme)} does not support type {type.FullName}.");
}

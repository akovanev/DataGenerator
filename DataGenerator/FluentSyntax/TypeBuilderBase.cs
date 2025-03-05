using System.Reflection;
using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.FluentSyntax;

public abstract class TypeBuilderBase
{
    protected TypeBuilderBase(Type type)
    {
        TypeProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(Property.CreateProperty)
            .ToDictionary(property => property.Name);
    }

    internal Dictionary<string, Property> TypeProperties { get; }
}
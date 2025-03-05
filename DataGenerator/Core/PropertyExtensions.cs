namespace Akov.DataGenerator.Core;

internal static class PropertyExtensions
{
    public static object? ExecuteConstructor(this Property property, object obj)
        => property.Constructor is null
            ? obj
            : property.Constructor.Compile()(obj);

    public static object? ExecuteDecorator(this Property property, object? obj)
        => property.Decorator is null
            ? obj
            : property.Decorator(obj);
}
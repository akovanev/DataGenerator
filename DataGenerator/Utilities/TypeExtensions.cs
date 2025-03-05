using System.Collections;

namespace Akov.DataGenerator.Utilities;

internal static class TypeExtensions
{
    public static (Type type, bool IsNullable) GetTypeOrNullableType(this Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type);
        return underlyingType is null
            ? (type, false)
            : (underlyingType, true);
    }
    
    public static bool IsClassExceptString(this Type type)
        => type != typeof(string) && type.IsClass;

    public static bool IsEnumerableExceptString(this Type type)
        => type != typeof(string) && type.GetInterface(nameof(IEnumerable)) is not null;
}
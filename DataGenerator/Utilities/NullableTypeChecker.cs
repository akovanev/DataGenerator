using System.Reflection;

namespace Akov.DataGenerator.Utilities;

internal static class NullableTypeChecker
{
#if NET5_0_OR_GREATER
    private static readonly NullabilityInfoContext NullabilityContext = new();
#endif

    public static bool IsNullableReferenceType(PropertyInfo property)
    {
#if NET5_0_OR_GREATER
        var nullabilityInfo = NullabilityContext.Create(property);
        return nullabilityInfo.WriteState == NullabilityState.Nullable;
#else
        return true;
#endif
    }
}
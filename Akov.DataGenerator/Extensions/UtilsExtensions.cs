using System;

namespace Akov.DataGenerator.Extensions;

public static class UtilsExtensions
{
    public static string ToCamelCase(this string source)
        => string.Create(source.Length, source, (chars, state) =>
        {
            state.AsSpan().CopyTo(chars);
            chars[0] = char.ToLower(chars[0]);
        });
}
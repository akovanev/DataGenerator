using System;
using System.Collections.Generic;
using System.Linq;

namespace Akov.DataGenerator.Extensions;

internal static class AttributeExtensions
{
    public static T? GetValue<T>(this IEnumerable<Attribute>? attrs)
        where T : Attribute
    {
        return attrs?.OfType<T>().SingleOrDefault();
    }
}
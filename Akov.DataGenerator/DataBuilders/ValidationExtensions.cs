using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Akov.DataGenerator.DataBuilders
{
    internal static class ValidationExtensions
    {
        internal static void ThrowIfNull<T>(this T source, string message)
        {
            if(source is null)
                throw new ArgumentNullException(message);
        }

        internal static void ThrowIfNullOrEmpty(this string? source, string message)
        {   
            if(String.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(message);
        }

        internal static void ThrowIfNullOrEmpty<T>(this IEnumerable<T>? source, string message)
        {
            source.ThrowIfNull(message);
            
            if(!source.Any())
                throw new ArgumentException($"Collection is empty. {message}");
        }

        internal static void ThrowIfNotTrue(this bool exprResult, string message)
        {
            if(!exprResult)
                throw new NotSupportedException(message);
        }
    }
}
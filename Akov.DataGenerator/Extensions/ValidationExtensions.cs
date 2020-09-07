using System;
using System.Collections.Generic;
using System.Linq;

namespace Akov.DataGenerator.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNull<T>(this T source, string message)
        {
            if(source is null)
                throw new ArgumentNullException(message);
        }

        public static void ThrowIfNullOrEmpty(this string? source, string message)
        {   
            if(String.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(message);
        }

        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T>? source, string message)
        {
            source.ThrowIfNull(message);
            
            if(!source.Any())
                throw new ArgumentException($"Collection is empty. {message}");
        }

        public static void ThrowIfNotTrue(this bool exprResult, string message)
        {
            if(!exprResult)
                throw new NotSupportedException(message);
        }
    }
}
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

        public static void ThrowIfNullOrEmpty<T>(this List<T>? source, string message)
        {
            source.ThrowIfNull(message);
            
            if(!source!.Any())
                throw new ArgumentException($"Collection is empty. {message}");
        }

        public static void ThrowIfNegative(this int value, string message)
        {
            if (value < 0)
                throw new ArgumentException(message);
        }

        public static void ThrowIfNotInRange(this double value, double min, double max, string message)
        {
            if (value < min || value > max)
                throw new ArgumentException(message);
        }
    }
}
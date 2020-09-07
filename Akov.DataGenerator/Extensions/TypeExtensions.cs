using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Extensions
{
    internal static class TypeExtensions
    {
        public static void AddToPropsDictionary(
            this Type type,
            Dictionary<Type, PropertyInfo[]> propsDictionary)
        {
            if (propsDictionary.ContainsKey(type)) return;

            PropertyInfo[] props = type.GetProperties();

            propsDictionary.Add(type, props);

            var innerTypes = props
                .Select(p => GetPropertyType(p.PropertyType))
                .Where(p => p != null && !propsDictionary.ContainsKey(p))
                .ToList();

            foreach (var t in innerTypes)
            {
                AddToPropsDictionary(t!, propsDictionary);
            }
        }

        public static string? GetPropertyTemplateType(this Type type)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(type);

            if (!(underlyingType is null))
                type = underlyingType;

            if (type.IsEnum)
                return TemplateType.Set;

            if (type.IsArray || type.IsEnumerableExceptString())
                return TemplateType.Array;

            var templateTypeDictionary = GetTemplateTypeDictionary();

            return templateTypeDictionary.ContainsKey(type)
                ? templateTypeDictionary[type]
                : type.IsClass
                    ? TemplateType.Object
                    : null;
        }

        public static string GetArrayPatternTemplateType(this Type type)
        {
            Type patternType = type.GetArrayPatternType();
            var templateTypeDictionary = GetTemplateTypeDictionary();

            return templateTypeDictionary.ContainsKey(patternType)
                ? templateTypeDictionary[patternType]
                : patternType.Name;
        }

        public static bool IsInExistingTemplates(this string template)
        {
            return GetTemplateTypeDictionary().ContainsValue(template);
        }

        private static Type? GetPropertyType(this Type type)
        {
            while (true)
            {
                if (type.IsValueType ||
                    type == typeof(object) ||
                    type == typeof(string)) return null;

                if (type.IsArray)
                {
                    type = type.GetElementType()!;
                    continue;
                }

                if (!type.IsGenericType) return type;

                if (!type.IsEnumerableExceptString()) return null;

                type = type.GetGenericArguments()[0];
            }
        }

        private static Type GetArrayPatternType(this Type type)
        {
            Type? elementType = null;

            if (type.IsArray)
                elementType = type.GetElementType();
            else if (type.IsEnumerableExceptString())
            {
                elementType = type.GetGenericArguments()[0];
            }

            if (!(elementType is null))
            {
                return !elementType.IsEnum
                    ? elementType
                    : throw new NotSupportedException("Collections of enum type are not supported");
            }

            throw new NotSupportedException($"Can't get pattern for type {type}");
        }

        private static bool IsEnumerableExceptString(this Type type)
            => type != typeof(String) && type.GetInterface(nameof(IEnumerable)) != null;

        private static Dictionary<Type, string> GetTemplateTypeDictionary()
            => new Dictionary<Type, string>
            {
                {typeof(String), TemplateType.String},
                {typeof(Guid), TemplateType.Guid},
                {typeof(Boolean), TemplateType.Bool},
                {typeof(Int32), TemplateType.Int},
                {typeof(Double), TemplateType.Double},
                {typeof(DateTime), TemplateType.DateTime}
            };
    }
}

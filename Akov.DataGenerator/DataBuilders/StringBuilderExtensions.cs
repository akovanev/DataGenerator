using System;
using System.Text;

namespace Akov.DataGenerator.DataBuilders
{
    internal static class StringBuilderExtensions
    {
        internal static void InsertArray(this StringBuilder builder, string? name, Action action, bool isLastItem = true)
        {
            builder.InsertArrayBegin(name ?? "array");
            action();
            builder.InsertArrayEnd(isLastItem);
        }

        internal static void InsertObject(this StringBuilder builder, string? name, Action action, bool isLastItem)
        {
            builder.InsertObjectBegin(name);
            action();
            builder.InsertObjectEnd(isLastItem);
        }

        internal static void InsertArrayBegin(this StringBuilder builder, string name)
        {
            builder.Append($"\"{name}\":[");
        }

        internal static void InsertArrayEnd(this StringBuilder builder, bool isLastItem)
        {
            builder.Append(InsertEnd("]", isLastItem));
        }

        internal static void InsertObjectBegin(this StringBuilder builder, string? name)
        {
            builder.Append(String.IsNullOrEmpty(name)
                ? "{"
                : $"\"{name}\":{{");    
        }

        internal static void InsertObjectEnd(this StringBuilder builder, bool isLastItem)
        {
            builder.Append(InsertEnd("}", isLastItem));
        }

        internal static void InsertProperty(this StringBuilder builder, string? name, object? value, bool isLastItem)
        {
            name ??= "prop";
            builder.Append(value is null
                ? $"\"{name}\":null{InsertEnd("", isLastItem)}"
                : $"\"{name}\":\"{value}\"{InsertEnd("", isLastItem)}");
        }

        private static string InsertEnd(string end, bool isLastItem)
        {
            return !isLastItem
                ? $"{end},"
                : end;
        }
    }
}
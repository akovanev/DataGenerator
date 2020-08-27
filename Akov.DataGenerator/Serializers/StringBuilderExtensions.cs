using System;
using System.Collections.Generic;
using System.Text;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Serializers
{
    internal static class StringBuilderExtensions
    {
        internal static void AppendObjectBegin(this StringBuilder builder, string? name)
        {
            builder.Append(String.IsNullOrEmpty(name)
                ? "{"
                : $"\"{name}\":{{");
        }

        internal static void AppendObjectEnd(this StringBuilder builder, bool isLastItem)
        {
            builder.Append(InsertEnd("}", isLastItem));
        }

        internal static void AppendArrayBegin(this StringBuilder builder, string name)
        {
            builder.Append($"\"{name}\":[");
        }

        internal static void AppendArrayEnd(this StringBuilder builder, bool isLastItem)
        {
            builder.Append(InsertEnd("]", isLastItem));
        }

        internal static void AppendProperty(this StringBuilder builder, ValueObject value, bool isLastItem)
        {
            string name = value.Name ?? "prop";
            builder.Append($"\"{name}\":");

            if (value.Value is List<ValueObject>)
                builder.Append("[]");
            else if (value.Value is ValueObject)
                builder.Append("{}");
            else builder.Append(value.Value is null || value.Value is List<ValueObject>
                    ? "null"
                    : $"\"{value.Value}\"");

            builder.Append(InsertEnd("", isLastItem));
        }

        private static string InsertEnd(string end, bool isLastItem)
        {
            return !isLastItem
                ? $"{end},"
                : end;
        }
    }
}
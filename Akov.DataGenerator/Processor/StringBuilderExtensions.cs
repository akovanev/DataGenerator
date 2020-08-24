using System;
using System.Text;

namespace Akov.DataGenerator.Processor
{
    internal static class StringBuilderExtensions
    {
        internal static void InsertArray(this StringBuilder builder, string name, Action action, bool isLastItem = true)
        {
            builder.InsertArrayBegin(name);
            action();
            builder.InsertArrayEnd(isLastItem);
        }

        internal static void InsertObject(this StringBuilder builder, Action action, bool isLastItem = true)
        {
            builder.InsertObjectBegin();
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

        internal static void InsertObjectBegin(this StringBuilder builder)
        {
            builder.Append("{");
        }

        internal static void InsertObjectEnd(this StringBuilder builder, bool isLastItem)
        {
            builder.Append(InsertEnd("}", isLastItem));
        }

        internal static void InsertProperty(this StringBuilder builder, string name, object? value, bool isLastItem)
        {
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
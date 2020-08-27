using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Serializers
{
    public class JsonValueObjectSerializer
    {
        public static string Serialize(ValueObject value)
        {
            var builder = new StringBuilder();

            AppendObject(builder, value.Name, value.Value as List<ValueObject>, true);

            return builder.ToString();
        }

        internal static void AppendObject(StringBuilder builder, string name, List<ValueObject>? properties, bool isLastItem)
        {
            if(properties is null)
                throw new NotSupportedException($"Cannot cast {properties} to {nameof(List<ValueObject>)}");

            builder.AppendObjectBegin(name);

            for (int i = 0; i < properties.Count; i++)
            {
                AppendProperty(builder, properties[i], i == properties.Count - 1);
            }

            builder.AppendObjectEnd(isLastItem);
        }

        internal static void AppendProperty(StringBuilder builder, ValueObject value, bool isLastItem)
        {
            if (value.Value is List<ValueObject> valueObject)
            {
                ValueObject first = valueObject.FirstOrDefault();

                if (first == null)
                {
                    builder.AppendProperty(value, isLastItem);
                    return;
                }

                if (first.Value is List<ValueObject>)
                {
                    AppendArray(builder, value.Name, valueObject, isLastItem);
                }
                else
                {
                    AppendObject(builder, value.Name, valueObject, isLastItem);
                }
            }
            else
            {
                builder.AppendProperty(value, isLastItem);
            }
        }

        internal static void AppendArray(StringBuilder builder, string name, List<ValueObject> valueList, bool isLastItem)
        {
            builder.AppendArrayBegin(name);

            for (int i = 0; i < valueList.Count; i++)
            {
                AppendObject(builder, "", valueList[i].Value as List<ValueObject>, i == valueList.Count - 1);
            }

            builder.AppendArrayEnd(isLastItem);
        }
    }
}

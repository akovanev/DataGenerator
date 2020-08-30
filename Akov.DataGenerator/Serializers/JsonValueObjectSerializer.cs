using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Serializers
{
    public class JsonValueObjectSerializer
    {
        public static string Serialize(NameValueObject value)
        {
            var builder = new StringBuilder();

            AppendObject(builder, value.Name, value.Value as List<NameValueObject>, true);

            return builder.ToString();
        }

        internal static void AppendObject(StringBuilder builder, string name, List<NameValueObject>? properties, bool isLastItem)
        {
            if(properties is null)
                throw new NotSupportedException($"Cannot cast {properties} to {nameof(List<NameValueObject>)}");

            builder.AppendObjectBegin(name);

            for (int i = 0; i < properties.Count; i++)
            {
                AppendProperty(builder, properties[i], i == properties.Count - 1);
            }

            builder.AppendObjectEnd(isLastItem);
        }

        internal static void AppendProperty(StringBuilder builder, NameValueObject value, bool isLastItem)
        {
            if (value.Value is List<NameValueObject> valueObject)
            {
                NameValueObject first = valueObject.FirstOrDefault();

                if (first is null)
                {
                    builder.AppendProperty(value, isLastItem);
                }
                else
                {
                    if (first.Value is List<NameValueObject>)
                    {
                        AppendArray(builder, value.Name, valueObject, isLastItem);
                    }
                    else
                    {
                        AppendObject(builder, value.Name, valueObject, isLastItem);
                    }
                }
            }
            else
            {
                builder.AppendProperty(value, isLastItem);
            }
        }

        internal static void AppendArray(StringBuilder builder, string name, List<NameValueObject> valueList, bool isLastItem)
        {
            builder.AppendArrayBegin(name);

            for (int i = 0; i < valueList.Count; i++)
            {
                AppendObject(builder, "", valueList[i].Value as List<NameValueObject>, i == valueList.Count - 1);
            }

            builder.AppendArrayEnd(isLastItem);
        }
    }
}

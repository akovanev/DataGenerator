using System.Linq;
using System;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Generators
{
    public class StringGenerator : GeneratorBase
    {
        private const string DefaultPattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int DefaultMinLength = 10;
        private const int DefaultMaxLength = 20;
        private const int DefaultMinSpaceCount = 0;
        private const int DefaultMaxSpaceCount = 1;

        protected  override object CreateImpl(PropertyObject propertyObject)
        {
            Property property = propertyObject.Property;
            int minLength = property.MinLength ?? DefaultMinLength;
            int maxLength = property.MaxLength ?? DefaultMaxLength;
            int minSpaceCount = property.MinSpaceCount ?? DefaultMinSpaceCount;
            int maxSpaceCount = property.MaxSpaceCount ?? DefaultMaxSpaceCount;

            Random random = GetRandomInstance(propertyObject);
            int length = random.GetInt(minLength, maxLength);
            int spaces = random.GetInt(minSpaceCount, maxSpaceCount);
            string pattern = string.IsNullOrWhiteSpace(property.Pattern)
                ? DefaultPattern
                : property.Pattern;
            return CreateString(pattern, length, spaces);
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            Property property = propertyObject.Property;
            int minLength = property.MinLength ?? DefaultMinLength;
            int maxLength = property.MaxLength ?? DefaultMaxLength;
            
            Random random = GetRandomChoiceInstance();

            int length = random.GetInt(0, 1) == 0
                ? random.GetInt(0, minLength - 1)
                : random.GetInt(maxLength + 1, maxLength * 2);

            string pattern = string.IsNullOrWhiteSpace(property.Pattern)
                ? DefaultPattern
                : property.Pattern;

            return CreateString(pattern, length, DefaultMinSpaceCount);
        }

        private string CreateString(string pattern, int length, int spaces)
        {
            Random random = GetRandomChoiceInstance(); //todo: should be improved
            int[] patternIndexes = random.GetSequence(pattern.Length - 1, length);
            int[] spaceIndexes = random.GetSequence(length - 1, spaces);

            char[] value = new char[length];
            
            for (var i = 0; i < length; i++)
            {
                value[i] = spaceIndexes.Contains(i)
                    ? ' '
                    : pattern[patternIndexes[i]];
            }

            return new string(value);
        }
    }
}

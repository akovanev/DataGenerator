using System.Linq;
using Akov.DataGenerator.Failures;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processor
{
    internal class StringGenerator : GeneratorBase
    {
        private const string DefaultPattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int DefaultMinLength = 10;
        private const int DefaultMaxLength = 20;
        private const int DefaultMinSpaceCount = 0;
        private const int DefaultMaxSpaceCount = 1;

        protected internal override object CreateImpl(Property property, Template template)
        {
            int minLength = property.MinLength ?? DefaultMinLength;
            int maxLength = property.MaxLength ?? DefaultMaxLength;
            int minSpaceCount = property.MinSpaceCount ?? DefaultMinSpaceCount;
            int maxSpaceCount = property.MaxSpaceCount ?? DefaultMaxSpaceCount;

            int length = GetRandom(minLength, maxLength);
            int spaces = GetRandom(minSpaceCount, maxSpaceCount);
            string pattern = string.IsNullOrWhiteSpace(template.Pattern)
                ? DefaultPattern
                : template.Pattern;
            return CreateString(pattern, length, spaces);
        }

        protected internal override object? CreateFailureImpl(Property property, Template template, FailureType failureType)
        {
            if (failureType == FailureType.Nullable) return null;

            //Todo: add logic here

            return "@!$%*";
        }

        internal string CreateString(string pattern, int length, int spaces)
        {
            int[] patternIndexes = GetRandomSequence(pattern.Length - 1, length);
            int[] spaceIndexes = GetRandomSequence(length - 1, spaces);

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

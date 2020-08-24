using System;
using System.Linq;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processor
{
    internal abstract class GeneratorBase
    {
        private static readonly Random Random = new Random();

        internal object? Create(Property property, Template template, int index)
        {
            var failure = CreateFailureImpl(property, index);

            return failure.success
                ? failure.value
                : CreateImpl(property, template, index);
        }

        protected internal abstract object CreateImpl(Property property, Template template, int index);
       
        protected internal (bool success, object? value) CreateFailureImpl(Property property, int index)
        {
            Failure? failure = property.Failure;

            if (failure is null) return (false, null);

            if (failure.Nullable.HasValue && (index + 1) % failure.Nullable.Value == 0)
                return (true, null);

            if (failure.Invalid.HasValue && (index + 1) % failure.Invalid.Value == 0)
                return (true, "!@~");

            return (false, null);
        }
        
        protected internal int GetRandom(int min, int max)
        {
            return Random.Next(min, max);
        }

        protected internal double GetRandomDouble(double min, double max)
        {
            return min + Random.NextDouble() * (max - min);
        }

        protected internal int[] GetRandomSequence(int max, int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => Random.Next(max))
                .ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Failures;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public abstract class GeneratorBase : IGenerator
    {
        private static readonly Random Random = new Random();

        public object? Create(Property property, Template template)
        {
            FailureType failureType = GetFailureType(property.Failure);
            
            return failureType == FailureType.None
                ? CreateImpl(property, template)
                : CreateFailureImpl(property, template, failureType);
        }

        protected internal abstract object CreateImpl(Property property, Template template);
        protected internal abstract object CreateRangeFailureImpl(Property property, Template template);

        protected internal object? CreateFailureImpl(Property property, Template template, FailureType failureType)
        {
            return failureType switch
            {
                FailureType.Nullable => null,
                FailureType.Range => CreateRangeFailureImpl(property, template),
                _ => "@!$%*",
            };
        }

        protected internal FailureType GetFailureType(Failure? failure)
        {
            List<FailureObject> failureObjectList = failure.ToFailureObjectList();

            double random = GetRandomDouble(0, 1);

            return failureObjectList.GetFailureType(random);
        }

        protected internal int GetRandom(int min, int max)
        {
            return Random.Next(min, max + 1);
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
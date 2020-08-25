using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Failures
{
    internal static class FailureObjectExtensions
    {
        internal static List<FailureObject> ToFailureObjectList(this Failure? failure)
        {
            double nullProbability = failure?.Nullable ?? 0;
            double invalidProbability = failure?.Invalid ?? 0;

            if(new [] {nullProbability, invalidProbability }.Any(x => x < 0 || x > 1))
                throw new ArgumentException("Probability should not be less than zero or greater than 1");

            double noneProbability = 1 - nullProbability - invalidProbability;

            return new List<FailureObject>
            {
                new FailureObject(FailureType.None, new Range(0, noneProbability)),
                new FailureObject(FailureType.Nullable, new Range(noneProbability, noneProbability + nullProbability)),
                new FailureObject(FailureType.Invalid, new Range(noneProbability + nullProbability, 1))
            };
        }
       
        internal static FailureType GetFailureType(this List<FailureObject> list, double value)
        {
            return list.First(f => f.RandomRage.In(value)).FailureType;
        }
    }
}
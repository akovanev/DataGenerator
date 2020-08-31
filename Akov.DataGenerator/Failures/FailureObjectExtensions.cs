using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Failures
{
    internal static class FailureObjectExtensions
    {
        public static List<FailureObject> ToFailureObjectList(this Failure? failure)
        {
            double customProbability = failure?.Custom ?? 0;
            double nullProbability = failure?.Nullable ?? 0;
            double rangeProbability = failure?.Range ?? 0;

            double[] group = {customProbability, nullProbability, rangeProbability};

            if (group.Any(x => x < 0 || x > 1))
                throw new ArgumentException("Probability should not be less than zero or greater than 1");

            double noneProbability = 1 - group.Sum();

            if (noneProbability < 0)
                throw new ArgumentException("Sum of all probabilities should not be greater than 1");

            var sums = new double[group.Length + 1];
            sums[0] = noneProbability;
            for (int i = 1; i < sums.Length; i++)
                sums[i] = sums[i - 1] + group[i - 1];

            return new List<FailureObject>
            {
                new FailureObject(FailureType.None, new Range(0, sums[0])),
                new FailureObject(FailureType.Custom, new Range(sums[0], sums[1])),
                new FailureObject(FailureType.Nullable, new Range(sums[1], sums[2])),
                new FailureObject(FailureType.Range, new Range(sums[2], sums[3]))
            };
        }
       
        public static FailureType GetFailureType(this List<FailureObject> list, double value)
        {
            return list.First(f => f.RandomRage.In(value)).FailureType;
        }
    }
}
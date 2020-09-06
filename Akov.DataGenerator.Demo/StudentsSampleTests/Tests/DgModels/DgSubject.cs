using System.Collections.Generic;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels
{
    public class DgSubject
    {
        [DgPattern("abcdefghijklmnopqrstuvwxyz0123456789")]
        [DgLength(Min = 10, Max = 10)]
        public string? EncodedDescription { get; set; }

        [DgRange(Min = 1, Max = 10)]
        public int Attempts { get; set; }

        public bool IsPassed { get; set; }

        [DgSubTypePattern("0.00")]
        [DgRange(Min = 0, Max = 125.0)]
        [DgLength(Max = 2)]
        [DgFailure(
            NullProbability = 0.15,
            CustomProbability = 0.075,
            OutOfRangeProbability = 0.05)]
        public List<double>? TotalPrices { get; set; }
    }
}
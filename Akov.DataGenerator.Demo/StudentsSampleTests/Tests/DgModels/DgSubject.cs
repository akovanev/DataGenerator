using System.Collections.Generic;
using Akov.DataGenerator.Attributes;
using Akov.DataGenerator.Generators;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels
{
    /// <summary>
    /// Represents the data generation process for the Subject response model.
    /// </summary>
    public class DgSubject
    {
        [DgName("encoded_description")]
        [DgPattern(StringGenerator.AbcLowerNum)]
        [DgLength(Min = 10, Max = 10)]
        public string? EncodedDescription { get; set; }

        [DgRange(Min = 1, Max = 10)]
        public int Attempts { get; set; }

        public bool IsPassed { get; set; }

        [DgName("total_prices")]
        [DgSubTypePattern("0.00")]
        [DgRange(Min = 0, Max = 125.0)]
        [DgLength(Max = 2)]
        [DgFailure(
            NullProbability = 0.15,
            CustomProbability = 0.2,
            OutOfRangeProbability = 0.05)]
        [DgCustomFailure("####")]
        public List<double>? TotalPrices { get; set; }
    }
}
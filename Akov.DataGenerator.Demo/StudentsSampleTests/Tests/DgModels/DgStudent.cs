using System;
using System.Collections.Generic;
using Akov.DataGenerator.Attributes;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels
{
    /// <summary>
    /// Represents the data generation process for the Student response model.
    /// </summary>
    public class DgStudent
    {
        [DgFailure(NullProbability = 0.2)]
        public Guid Id { get; set; }

        [DgSource("firstnames.txt")]
        [DgFailure(NullProbability = 0.1)]
        public string? FirstName { get; set; }

        [DgSource("lastnames.txt")]
        [DgFailure(NullProbability = 0.1)]
        public string? LastName { get; set; }

        [DgCalc] //supposed to be calculated
        public string? FullName { get; set; }
        
        [DgGenerator(GeneratorType.Phone)]
        [DgPattern("+45 ## ## ## ##;+420 ### ### ###")]
        [DgFailure(NullProbability = 0.05)]
        public string? Phone { get; set; }
        
        [DgGenerator(StudentGeneratorFactory.UintGenerator)]
        [DgRange(Max = 5)]
        public int Year { get; set; }

        [DgName("test_variant")]
        public Variant Variant { get; set; }

        [DgName("test_answers")]
        [DgRange(Min = 1, Max = 5)]
        [DgLength(Max = 5)]
        public int[]? TestAnswers { get; set; }

        [DgName("encoded_solution")]
        [DgPattern("abcdefghijklmnopqrstuvwxyz0123456789")]
        [DgLength(Min = 15, Max = 50)]
        [DgSpacesCount(Min = 1, Max = 3)]
        [DgFailure(
            NullProbability = 0.1,
            CustomProbability = 0.1,
            OutOfRangeProbability = 0.05)]
        [DgCustomFailure("####-####-####")]
        public string? EncodedSolution { get; set; }

        [DgName("last_updated")]
        [DgPattern("dd/MM/yy")]
        [DgRange(Min = "20/10/19", Max = "01/01/20")]
        [DgFailure(
            NullProbability = 0.2,
            CustomProbability = 0.2,
            OutOfRangeProbability = 0.1)]
        public DateTime? LastUpdated { get; set; }

        public List<DgSubject>? Subjects { get; set; }

        public DgSubject? Subject { get; set; }
        
        [DgPattern("##.##")]
        [DgRange(Min = 9.50, Max = 99.50)]
        public Decimal Discount { get; set; }
        
        [DgLength(Min = 4, Max = 16)]
        [DgFailure(NullProbability = 0.1)]
        public byte[]? Signature { get; set; }
    }
}

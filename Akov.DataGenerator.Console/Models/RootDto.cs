using System;
using System.Collections.Generic;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Models
{
    class RootDto
    {
        [DgCalc]
        public int? Count { get; set; }

        [DgLength(Max = 5)]
        public List<StudentDto>? Students { get; set; }
    }
 
    class StudentDto
    {
        public string? Id { get; set; }

        [DgSource("firstnames.txt")]
        [DgFailure(NullProbability = 0.1)]
        public string? FirstName { get; set; }

        [DgSource("lastnames.txt")]
        [DgFailure(NullProbability = 0.1)]
        public string? LastName { get; set; }

        [DgCalc] //supposed to be calculated
        public string? FullName { get; set; }

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
            NullProbability = 0.1,
            CustomProbability = 0.2,
            OutOfRangeProbability = 0.1)]
        public DateTime? LastUpdated { get; set; }

        public List<SubjectDto>? Subjects { get; set; }

        public SubjectDto? Subject { get; set; }
    }

    class SubjectDto
    {
        [DgName("encoded_description")]
        [DgPattern("abcdefghijklmnopqrstuvwxyz0123456789")]
        [DgLength(Min = 10, Max = 10)]
        public string? EncodedDescription { get; set; }

        [DgRange(Min = 1, Max = 10)]
        [DgFailure(
            NullProbability = 0.05, 
            CustomProbability = 0.1, 
            OutOfRangeProbability = 0.1)]
        public int Attempts { get; set; }

        [DgFailure(
            NullProbability = 0.05,
            CustomProbability = 0.1,
            OutOfRangeProbability = 0.1)]
        public bool IsPassed { get; set; }

        [DgName("total_price")]
        [DgSubTypePattern("0.00")]
        [DgRange(Min = 0, Max = 125.0)]
        [DgLength(Max = 2)]
        [DgFailure(
            NullProbability = 0.15,
            CustomProbability = 0.075,
            OutOfRangeProbability = 0.05)]
        public List<double>? TotalPrices { get; set; }
    }

    enum Variant
    {
        A,
        B,
        C,
        D,
        E
    }
}

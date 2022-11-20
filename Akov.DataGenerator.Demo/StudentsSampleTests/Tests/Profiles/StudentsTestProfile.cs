using System;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;

public class StudentsTestProfile : DgProfileBase
{
    public StudentsTestProfile()
    {
        ForType<StudentCollection>()
            .Property(c => c.Count).IsCalc()
            .Property(c => c.Students).Length(100, 100);
                
        ForType<Student>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings)
            .Property(s => s.Id).Failure(nullable: 0.2)
            .Property(s => s.FirstName).FromFile("firstnames.txt").Failure(nullable: 0.1)
            .Property(s => s.LastName).FromFile("lastnames.txt").Failure(nullable: 0.1)
            .Property(s => s.FullName).Assign(s => $"{s.FirstName} {s.LastName}")
            .Property(s => s.Year).UseGenerator(StudentGeneratorFactory.UintGenerator).Range(5)
            .Property(s => s.Variant).HasJsonName("test_variant")
            .Property(s => s.TestAnswers).HasJsonName("test_answers").Length(5).Range(1, 5)
            .Property(s => s.EncodedSolution).HasJsonName("encoded_solution")
                .Pattern("abcdefghijklmnopqrstuvwxyz0123456789").Length(15, 50).Spaces(1,3)
                .Failure(0.1, 0.1, 0.05, "####-####-####" )
            .Property(s => s.LastUpdated).HasJsonName("last_updated").Pattern("dd/MM/yy")
                .Range("20/10/19","01/01/20").Failure(0.2, 0.2, 0.1)
            .Property(s => s.Subjects).Length(4)
            .Property(s => s.Discount).Pattern("##.##").Range(9.50, 99.50);

        ForType<Subject>()
            .Property(s => s.EncodedDescription).HasJsonName("encoded_description")
                .Pattern("abcdefghijklmnopqrstuvwxyz0123456789").Length(10, 20)
            .Property(s => s.Attempts).Range(1, 10)
            .Property(s => s.TotalPrices).HasJsonName("total_prices").SubTypePattern("0.00")
                .Range(0, 125.0).Length(2).Failure(0.15, 0.2, 0.05, "####");
    }
}
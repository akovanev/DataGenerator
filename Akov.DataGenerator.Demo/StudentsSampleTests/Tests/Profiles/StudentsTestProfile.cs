using System;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;

public class StudentsTestProfile : DgProfileBase
{
    public StudentsTestProfile()
    {
        ForType<StudentCollection>()
            .ForProperty(c => c.Count)
                .WithValue(new Property {Type = TemplateType.Calc})
            .ForProperty(c => c.Students)
                .WithValue(new Property
                {
                    Type = TemplateType.Array,
                    Pattern = nameof(Student),
                    MinLength = 100,
                    MaxLength = 100
                });
        
        ForType<Student>()
            .ForProperty(s => s.Id)
                .WithValue(new Property {Type = TemplateType.Guid})
                .WithFailure(new Failure {Nullable = 0.2})
            .ForProperty(s => s.FirstName)
                .WithValue(new Property
                {
                    Type = TemplateType.File,
                    Pattern = "firstnames.txt"
                })
               .WithFailure(new Failure {Nullable = 0.1})
            .ForProperty(s => s.LastName)
                .WithValue(new Property
                {
                    Type = TemplateType.File,
                    Pattern = "lastnames.txt"
                })
                .WithFailure(new Failure {Nullable = 0.1})
            .ForProperty(s => s.FullName)
                .WithValue(new Property {Type =TemplateType.Calc})
            .ForProperty(s => s.Variant)
                .WithValue(new Property
                {
                    Name = "test_variant",
                    Type = TemplateType.Set,
                    Pattern = string.Join(",", Enum.GetNames<Variant>())
                })
            .ForProperty(s => s.TestAnswers)
                .WithValue(new Property
                {
                    Name = "test_answers",
                    Type = TemplateType.Array,
                    Pattern = TemplateType.Int,
                    MinValue = 1,
                    MaxValue = 5,
                    MaxLength = 5
                })
            .ForProperty(s => s.EncodedSolution)
                .WithValue(new Property
                {
                    Name = "encoded_solution",
                    Type = TemplateType.String,
                    Pattern = "abcdefghijklmnopqrstuvwxyz0123456789",
                    MinLength = 15,
                    MaxLength = 50,
                    MinSpaceCount = 1,
                    MaxSpaceCount = 3
                })
                .WithFailure(new Failure {Nullable = 0.1, Custom = 0.1, Range = 0.05}, "####-####-####")
            .ForProperty(s => s.LastUpdated)
                .WithValue(new Property
                {
                    Name = "last_updated",
                    Type = TemplateType.DateTime,
                    Pattern = "dd/MM/yy",
                    MinValue = "20/10/19",
                    MaxValue = "01/01/20"
                })
                .WithFailure(new Failure {Nullable = 0.2, Custom = 0.2, Range = 0.1})
            .ForProperty(s => s.Subject)
                .WithValue(new Property
                {
                    Type = TemplateType.Object,
                    Pattern = nameof(Subject)
                })
            .ForProperty(s => s.Subjects)
                .WithValue(new Property
                {
                    Type = TemplateType.Array,
                    Pattern = nameof(Subject),
                    MaxLength = 4
                });

        ForType<Subject>()
            .ForProperty(s => s.EncodedDescription)
                .WithValue(new Property
                {
                    Name = "encoded_description",
                    Type = TemplateType.String,
                    Pattern = "abcdefghijklmnopqrstuvwxyz0123456789",
                    MinLength = 10,
                    MaxLength = 20
                })
            .ForProperty(s => s.Attempts)
                .WithValue(new Property
                {
                    Type = TemplateType.Int,
                    MinValue = 1,
                    MaxValue = 10
                })
            .ForProperty(s => s.IsPassed)
                .WithValue(new Property{Type = TemplateType.Bool})
            .ForProperty(s => s.TotalPrices)
                .WithValue(new Property
                {
                    Name = "total_prices",
                    Type = TemplateType.Array,
                    Pattern = TemplateType.Double,
                    SubTypePattern = "0.00",
                    MinValue = 0,
                    MaxValue = 125.0,
                    MinLength = 2
                })
                .WithFailure(new Failure {Nullable = 0.15, Custom = 0.2, Range = 0.05}, "####");
    }
}
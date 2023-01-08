using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Profiles;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;

public class StudentTypeTestProfile : DgProfileBase
{
    public StudentTypeTestProfile()
    {
        ForType<Student>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings)
            .Ignore(s => s.CompanyAddress);

        ForType<Subject>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings);
        
        ForType<Address>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings);
    }
}
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Profiles;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;

public class StudentTypeTestProfile : DgProfileBase
{
    public StudentTypeTestProfile(bool createDefaultProfilesForMissingTypes = false)
        : base(createDefaultProfilesForMissingTypes)
    {
        ForType<Student>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings)
            .Ignore(s => s.CompanyAddress);

        ForType<Address>()
            .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
            .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings);
    }
}
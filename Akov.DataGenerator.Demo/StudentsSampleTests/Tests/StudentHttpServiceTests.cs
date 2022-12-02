using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSample.Services;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using Akov.DataGenerator.Profiles;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests;

/// <summary>
/// The test class for the StudentHttpService.
/// </summary>
public class StudentHttpServiceTests
{
    private readonly DgProfileBase _profile = new StudentsTestProfile();
        
    [Theory]
    [InlineData(GenerationType.UseAttributes)]
    [InlineData(GenerationType.UseProfile)]
    public async Task<List<Student>> GetAll_RandomStudentList(GenerationType type)
    {
        var httpClient = new MockHttpClientFactory(type, _profile).GetStudentServiceClient();
        var studentService = new StudentHttpService(httpClient);
            
        var students = (await studentService.GetAll()).ToList();

        Assert.NotNull(students);

        //Expects only valid data returned.
        Assert.True(students.All(s => s.IsValid));

        //Due to the requirements the failure during casting to the LastUpdated is not considered to be an error.
        //But considered to be a warning.
        const string lastUpdatedJsonName = "last_updated";
            
        var studentsWithNotParsedDate = students
            .Where(s => s.HasWarnings && s.ParsingWarnings.ContainsKey(lastUpdatedJsonName))
            .ToList();

        //The fallback logic implies setting the today date in the case of failure.
        DateTime expectedDate = DateTime.Today;
        Assert.True(studentsWithNotParsedDate.All(x => x.LastUpdated == expectedDate));
        
        return students;
    }
}
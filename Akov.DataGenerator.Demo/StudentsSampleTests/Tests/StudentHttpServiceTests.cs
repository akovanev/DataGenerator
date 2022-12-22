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
        using var mockHttpClientFactory = new MockHttpClientFactory(type, _profile);
        var httpClient = mockHttpClientFactory.GetStudentServiceClient();
        var studentService = new StudentHttpService(httpClient);
            
        var students = (await studentService.GetAll()).ToList();

        Assert.NotNull(students);

        //Expects only valid data returned.
        Assert.True(students.All(s => s.IsValid));

        //Due to the requirements the failure during casting to the LastUpdated is not considered to be an error.
        //But considered to be a warning.
        const string lastUpdatedJsonName = "last_updated";

        var studentsWithNotParsedDate = students
            .Where(s => s.HasWarnings && s.ParsingWarnings.ContainsKey(lastUpdatedJsonName));

        //The fallback logic implies setting the today date in the case of failure.
        DateTime expectedDate = DateTime.Today;
        Assert.True(studentsWithNotParsedDate.All(x => x.LastUpdated == expectedDate));
        
        // Arrange repeat
        using var mockHttpClientRepeatFactory = new MockHttpClientFactory(type, _profile, true);
        var httpClientRepeat = mockHttpClientRepeatFactory.GetStudentServiceClient();
        studentService = new StudentHttpService(httpClientRepeat);
            
        var studentsRepeatRun = await studentService.GetAll();
        
        // Assert
        Assert.NotNull(studentsRepeatRun);
        Assert.True(studentsRepeatRun.All(s => s.IsValid));
        
        Assert.Equal(students.First().FullName, studentsRepeatRun.First().FullName);
        Assert.Equal(students.Last().FullName, studentsRepeatRun.Last().FullName);

        return students;
    }
}
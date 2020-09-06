using System;
using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.Services;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests
{
    /// <summary>
    /// The test class for the StudentService.
    /// </summary>
    public class StudentServiceTests
    {
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            var httpClient = new MockHttpClientFactory().GetStudentServiceClient();
            _studentService = new StudentService(httpClient);
        }

        [Fact]
        public async Task GetAll_RandomStudentList()
        {
            var students = (await _studentService.GetAll()).ToList();

            Assert.NotNull(students);

            //Expects only valid data returned.
            Assert.True(students.All(s => s.IsValid));

            //Due to the Requirements the failure during casting to the LastUpdated is not considered to be an error.
            const string lastUpdatedJsonName = "last_updated";
            Assert.True(students.All(s => !s.ParsingErrors.ContainsKey(lastUpdatedJsonName)));
            
            //Bur considered to be a warning.
            var studentsWithNotParsedDate = students
                .Where(s => s.HasWarnings && s.ParsingWarnings.ContainsKey(lastUpdatedJsonName))
                .ToList();

            //The fallback logic implies setting the today date in the case of failure.
            DateTime expectedDate = DateTime.Today;
            Assert.True(studentsWithNotParsedDate.All(x => x.LastUpdated == expectedDate));
        }
    }
}

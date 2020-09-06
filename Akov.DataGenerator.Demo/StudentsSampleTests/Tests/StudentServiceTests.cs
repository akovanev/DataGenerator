using System;
using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.Services;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests
{
    public class StudentServiceTests
    {
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            var httpClient = new MockHttpClientProvider().GetStudentServiceClient();
            _studentService = new StudentService(httpClient);
        }

        [Fact]
        public async Task GetAll_RandomStudentList()
        {
            var students = (await _studentService.GetAll()).ToList();
            const string lastUpdatedJsonName = "last_updated";

            Assert.NotNull(students);
            Assert.True(students.All(s => s.IsValid));
            Assert.True(students.All(s => !s.ParsingErrors.ContainsKey(lastUpdatedJsonName)));

            var studentsWithNotParsedDate = students
                .Where(s => s.HasWarnings && s.ParsingWarnings.ContainsKey(lastUpdatedJsonName))
                .ToList();

            DateTime expectedDate = DateTime.Today;
            Assert.True(studentsWithNotParsedDate.All(x => x.LastUpdated == expectedDate));
        }
    }
}

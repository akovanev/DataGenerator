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
            var result = (await _studentService.GetAll()).ToList();

            Assert.NotNull(result);
            Assert.True(result.All(s => s.IsValid));
        }
    }
}

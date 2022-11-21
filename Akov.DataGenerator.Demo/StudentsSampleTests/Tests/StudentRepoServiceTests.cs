using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSample.Services;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Profiles;
using Moq;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests;

/// <summary>
/// The test class for the StudentRepoService.
/// </summary>
public class StudentRepoServiceTests
{
    private readonly DG _dg;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;

    public StudentRepoServiceTests()
    {
        _dg = new DG(
            new StudentGeneratorFactory(),
            new DataSchemeMapperConfig { UseCamelCase = true },
            new FileReadConfig { UseCache = true });
        _studentRepositoryMock = new Mock<IStudentRepository>();
    }
    
    [Theory]
    [InlineData(GenerationType.UseAttributes)]
    [InlineData(GenerationType.UseProfile)]
    public async Task GetAll_RandomStudentList(GenerationType type)
    {
        // Arrange
        var data = type is GenerationType.UseAttributes
            ? _dg.GenerateObjectCollection<Student>(_dg.GetFromType<DgStudent>(), 100)
            : _dg.GenerateObjectCollection<Student>(new StudentsTestProfile(), 100);

        _studentRepositoryMock.Setup(x => x.GetAll())
            .ReturnsAsync(data);

        var studentService = new StudentRepoService(_studentRepositoryMock.Object);
        
        // Act
        var students = (await studentService.GetAll()).ToList();
        
        // Assert
        Assert.NotNull(students);
        Assert.True(students.All(s => s.IsValid));
    }
}
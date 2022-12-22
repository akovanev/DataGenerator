using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSample.Services;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.RunBehaviors;
using Moq;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests;

/// <summary>
/// The test class for the StudentRepoService.
/// </summary>
public class StudentRepoServiceTests : IDisposable
{
    private readonly DG _dg;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<IStudentRepository> _studentRepositoryRepeatRunMock;
    private readonly IRunBehavior _runBehavior = new StoreToFileRunBehavior();

    public StudentRepoServiceTests()
    {
        _dg = new DG(
            new StudentGeneratorFactory(),
            new DataSchemeMapperConfig { UseCamelCase = true },
            new FileReadConfig { UseCache = true },
            _runBehavior);
        _studentRepositoryMock = new Mock<IStudentRepository>();
        _studentRepositoryRepeatRunMock = new Mock<IStudentRepository>();
    }
    
    [Theory]
    [InlineData(GenerationType.UseAttributes)]
    [InlineData(GenerationType.UseProfile)]
    public async Task<List<Student>> GetAll_RandomStudentList(GenerationType type)
    {
        // Arrange
        var data = type is GenerationType.UseAttributes
            ? _dg.GenerateObjectCollection<Student>(_dg.GetFromType<DgStudent>(), 1000)
            : _dg.GenerateObjectCollection<Student>(new StudentsTestProfile(), 1000);

        _studentRepositoryMock.Setup(x => x.GetAll())
            .ReturnsAsync(data);

        var studentService = new StudentRepoService(_studentRepositoryMock.Object);
        
        // Act
        var students = (await studentService.GetAll()).ToList();
        
        // Assert
        Assert.NotNull(students);
        Assert.True(students.All(s => s.IsValid));
        
        // Arrange
        var dataRepeatRun = type is GenerationType.UseAttributes
            ? _dg.GenerateObjectCollection<Student>(_dg.GetFromType<DgStudent>(), 100, true)
            : _dg.GenerateObjectCollection<Student>(new StudentsTestProfile(), 100, true);
        
        _studentRepositoryRepeatRunMock.Setup(x => x.GetAll())
            .ReturnsAsync(dataRepeatRun);

        studentService = new StudentRepoService(_studentRepositoryMock.Object);
        
        // Act
        var studentsRepeatRun = (await studentService.GetAll()).ToList();
        
        // Assert
        Assert.NotNull(studentsRepeatRun);
        Assert.True(studentsRepeatRun.All(s => s.IsValid));
        
        Assert.Equal(students.First().FullName, studentsRepeatRun.First().FullName);
        Assert.Equal(students.Last().FullName, studentsRepeatRun.Last().FullName);

        return students;
    }

    public void Dispose()
    {
        // Clear
        _runBehavior.ClearResult($"{nameof(Student)}_collection");
        _runBehavior.ClearResult($"{nameof(DgStudent)}_collection");
    }
}
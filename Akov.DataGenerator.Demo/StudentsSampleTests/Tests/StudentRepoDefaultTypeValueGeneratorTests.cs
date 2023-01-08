using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Xunit;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests;

public class StudentRepoDefaultTypeValueGeneratorTests
{
    private readonly DG _dg;

    public StudentRepoDefaultTypeValueGeneratorTests()
    {
        _dg = new DG(
            new DefaultTypeValueGeneratorFactory(),
            new DataSchemeMapperConfig { UseCamelCase = true },
            new FileReadConfig { UseCache = true });
    }

    [Fact]
    public string GetAllDefaultValues()
    {
        // Arrange
        var data = _dg.GenerateJson<Student>(new StudentTypeTestProfile());
            
        // Assert
        Assert.NotEmpty(data);
        
        // Todo: add assertions

        return data;
    }

}
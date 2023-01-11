using System;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Newtonsoft.Json;
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
        var data = _dg.GenerateJson<Student>(new StudentTypeTestProfile(true));
        var student = JsonConvert.DeserializeObject<Student>(data);
        
        // Assert
        Assert.NotNull(student);
        Assert.Equal(Guid.Empty, student.Id);
        Assert.Equal("string", student.FullName);
        Assert.Equal(0, student.Year);
        Assert.Null(student.CompanyAddress);
        Assert.Equal(0, (int)student.Variant);
        Assert.Equal(0, student.Discount);

        return data;
    }

}
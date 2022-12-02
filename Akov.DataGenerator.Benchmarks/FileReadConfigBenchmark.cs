using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using BenchmarkDotNet.Attributes;

namespace Akov.DataGenerator.Benchmarks;

[MemoryDiagnoser]
public class FileReadConfigBenchmark
{
    private readonly DG _generator1 = new (new StudentGeneratorFactory());
    private readonly DG _generator2 = new (new StudentGeneratorFactory(), fileReadConfig: new FileReadConfig { UseCache = true});

    [Benchmark]
    public StudentCollection GenerateObject()
    {
        return _generator1.GenerateObject<StudentCollection>(new StudentsTestProfile());
    }
    
    [Benchmark]
    public StudentCollection GenerateObject_FilesCached()
    {
        return _generator2.GenerateObject<StudentCollection>(new StudentsTestProfile());
    }
}
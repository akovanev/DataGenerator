// See https://aka.ms/new-console-template for more information

using Akov.DataGenerator;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Profiles;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
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
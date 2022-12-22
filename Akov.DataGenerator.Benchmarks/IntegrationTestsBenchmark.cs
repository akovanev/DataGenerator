using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests;
using BenchmarkDotNet.Attributes;

namespace Akov.DataGenerator.Benchmarks;

[MemoryDiagnoser]
public class IntegrationTestsBenchmark
{
    private readonly StudentHttpServiceTests _httpTest = new();
    private readonly StudentRepoServiceTests _repoTest = new();

    [Benchmark]
    public Task<List<Student>> HttpService_GetAll_RandomStudentList()
    {
        return _httpTest.GetAll_RandomStudentList(GenerationType.UseProfile);
    }
    
    [Benchmark]
    public Task<List<Student>> RepoService_GetAll_RandomStudentList()
    {
        return _repoTest.GetAll_RandomStudentList(GenerationType.UseProfile);
    }
}
using Akov.DataGenerator.Benchmarks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<IntegrationTestsBenchmark>(
    DefaultConfig.Instance
        .AddJob(Job.Default.WithBaseline(true)));
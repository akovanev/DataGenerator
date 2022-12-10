using Akov.DataGenerator.Benchmarks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

BenchmarkRunner.Run<IntegrationTestsBenchmark>(
    DefaultConfig.Instance
        .AddJob(
            Job.Default
                .WithBaseline(true)
                .WithToolchain(InProcessEmitToolchain.Instance)));
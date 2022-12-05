﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.RunBehaviors;
using Moq;
using Moq.Protected;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks;

/// <summary>
/// The factory for mock http clients.
/// </summary>
public class MockHttpClientFactory : IDisposable
{
    private readonly Lazy<HttpClient> _studentServiceHttpClient;
    private readonly IRunBehavior _runBehavior = new StoreToFileRunBehavior();

    public MockHttpClientFactory(GenerationType generationType, DgProfileBase? profile = null, bool useLast = false)
    {
        if (generationType is GenerationType.UseProfile && profile is null)
            throw new InvalidOperationException("Profile should be defined");
            
        _studentServiceHttpClient = new Lazy<HttpClient>(() =>
        {
            var dg = new DG(
                new StudentGeneratorFactory(),
                new DataSchemeMapperConfig { UseCamelCase = true },
                new FileReadConfig { UseCache = true },
                _runBehavior);

            //Creates json based on DgStudentCollection attributes or profiles.
            string jsonData = generationType is GenerationType.UseAttributes
                ? dg.GenerateJson(dg.GetFromType<DgStudentCollection>(), useLast)
                : dg.GenerateJson<StudentCollection>(profile!, useLast);
                
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    //Responds with the generated data.
                    Content = new StringContent(jsonData)
                })
                .Verifiable();

            return new HttpClient(handlerMock.Object);
        });
    }

    public HttpClient GetStudentServiceClient()
        => _studentServiceHttpClient.Value;

    public void Dispose()
    {
        _runBehavior.ClearResult(nameof(StudentCollection));
        _runBehavior.ClearResult(nameof(DgStudentCollection));
    }
}
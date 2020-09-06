using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Scheme;
using Moq;
using Moq.Protected;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Mocks
{
    public class MockHttpClientProvider
    {
        private readonly Lazy<HttpClient> _studentServiceHttpClient;

        public MockHttpClientProvider()
        {
            _studentServiceHttpClient = new Lazy<HttpClient>(() =>
                {
                    var dg = new DG(
                        new StudentGeneratorFactory(),
                        new DataSchemeMapperConfig { UseCamelCase = true });

                    DataScheme scheme = dg.GetFromType<DgStudentCollection>();
                    string jsonData = dg.GenerateJson(scheme);

                    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                    handlerMock.Protected()
                        // Setup the PROTECTED method to mock
                        .Setup<Task<HttpResponseMessage>>(
                            "SendAsync",
                            ItExpr.IsAny<HttpRequestMessage>(),
                            ItExpr.IsAny<CancellationToken>()
                        )
                        // prepare the expected response of the mocked http call
                        .ReturnsAsync(new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(jsonData)
                        })
                        .Verifiable();

                    return new HttpClient(handlerMock.Object);
                });
        }

        public HttpClient GetStudentServiceClient()
            => _studentServiceHttpClient.Value;
    }
}
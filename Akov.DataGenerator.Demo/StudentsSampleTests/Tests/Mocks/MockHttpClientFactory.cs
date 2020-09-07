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
    /// <summary>
    /// The factory for mock http clients.
    /// </summary>
    public class MockHttpClientFactory
    {
        private readonly Lazy<HttpClient> _studentServiceHttpClient;

        public MockHttpClientFactory()
        {
            _studentServiceHttpClient = new Lazy<HttpClient>(() =>
            {
                var dg = new DG(
                    new StudentGeneratorFactory(),
                    new DataSchemeMapperConfig { UseCamelCase = true });

                //Creates DataSchemt based on DgStudentCollection attributes.
                DataScheme scheme = dg.GetFromType<DgStudentCollection>();
                
                //Generates json random data.
                string jsonData = dg.GenerateJson(scheme);

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
    }
}
using System;
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

        public MockHttpClientFactory(GenerationType generationType, DgProfileBase? profile = null)
        {
            if (generationType is GenerationType.UseProfile && profile is null)
                throw new InvalidOperationException("Profile should be defined");
            
            _studentServiceHttpClient = new Lazy<HttpClient>(() =>
            {
                var dg = new DG(
                    new StudentGeneratorFactory(),
                    new DataSchemeMapperConfig { UseCamelCase = true },
                    new FileReadConfig { UseCache = true });

                //Creates json based on DgStudentCollection attributes or profiles.
                string jsonData = generationType is GenerationType.UseAttributes
                    ? dg.GenerateJson(dg.GetFromType<DgStudentCollection>())
                    : dg.GenerateJson<StudentCollection>(profile!);
                
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
        
        public enum GenerationType
        {
            UseAttributes,
            UseProfile
        }
    }
}
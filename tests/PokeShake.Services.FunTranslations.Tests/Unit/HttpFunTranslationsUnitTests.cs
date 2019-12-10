using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.FunTranslations.Tests.Unit
{
    /// <summary>
    /// The HttpFunTranslationsService unit tests
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    [Collection("HttpFunTranslationsService Unit Tests")]
    public class HttpFunTranslationsUnitTests
    {
        /// <summary>
        /// The test prefix
        /// </summary>
        public const string TestPrefix = "HttpFunTranslationsService - ";

        /// <summary>
        /// Determines whether this instance [can get translation asynchronous].
        /// </summary>
        [Fact(DisplayName = TestPrefix + "Can get translation")]
        public async Task CanGetTranslationAsync()
        {
            // Arrange

            // Avoid mocking logger factory and use the null logger factory instead
            var nullLoggerFactory = new NullLoggerFactory();

            // Mock the HTTP handler so that it always returns blastoise when GET is invoked
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ 'success': { 'total': 99 }, 'contents': { translated: 'My translated text' }}"),
                })
               .Verifiable();

            // Create a new HTTP client with the mock handler
            var clientMock = new HttpClient(handlerMock.Object);

            // Declare the configuration options for the service
            var options = Options.Create(new HttpFunTranslationsServiceOptions
            {
                BaseUrl = "http://unit-test-url.fun",
                TranslateEndpoint = "/api/translate/test/",
                ShakespeareTranslationEndpoint ="shakespeare.json"
            });

            // Create a new instance of the service under test
            var service = new HttpFunTranslationsService(clientMock, options, nullLoggerFactory);

            // Act
            var translationResult = await service.TranslateShakespeareAsync("text to translate");

            // Assert
            translationResult.Should().NotBeNull();
            translationResult.Success.Total.Should().Be(99);
            translationResult.Contents.Translated.Should().Be("My translated text");

            // Verify that POST http://unit-test-url.fun/api/translate/test/shakespeare.json?text=text to translate was called once
            handlerMock
                .Protected()
                .Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post && req.RequestUri == new Uri("http://unit-test-url.fun/api/translate/test/shakespeare.json?text=text to translate")),
               ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}

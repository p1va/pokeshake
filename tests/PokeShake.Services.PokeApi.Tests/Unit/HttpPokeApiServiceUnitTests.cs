using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using PokeShake.Services.PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.PokeApi.Tests.Unit
{
    /// <summary>
    /// The HttpPokeApiService unit tests class
    /// </summary>
    [Collection("HttpPokeApiService Unit Tests")]
    public class HttpPokeApiServiceUnitTests
    {
        /// <summary>
        /// The prefix
        /// </summary>
        public const string Prefix = "HttpPokeApiService - ";

        /// <summary>
        /// Determines whether this instance [can get pokemon asynchronous].
        /// </summary>
        [Fact(DisplayName = Prefix + "Can get pokemon")]
        public async Task CanGetPokemonAsync()
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
                    Content = new StringContent("{'id':99,'name':'blastoise'}"),
                })
               .Verifiable();

            // Create a new HTTP client with the mock handler
            var clientMock = new HttpClient(handlerMock.Object);

            // Declare the configuration options for the service
            var options = Options.Create(new HttpPokeApiServiceOptions
            {
                BaseUrl = "http://unit-test-url.fun",
                SpeciesEndpoint = "/api/pokemon/"
            });

            // Create a new instance of the service under test
            var service = new HttpPokeApiService(clientMock, options, nullLoggerFactory);

            // Act
            var species = await service.GetPokemonAsync("blastoise");

            // Assert
            species.Should().NotBeNull();
            species.Id.Should().Be(99);
            species.Name.Should().Be("blastoise");

            // Verify that GET http://unit-test-url.fun/api/pokemon/blastoise was called once
            handlerMock
                .Protected()
                .Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://unit-test-url.fun/api/pokemon/blastoise")),
               ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}

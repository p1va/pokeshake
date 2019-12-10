using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokeShake.Services.FunTranslations.Contracts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.FunTranslations.Tests.Integration
{
    /// <summary>
    /// The HttpFunTranslationsService integration tests
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    [Collection("HttpFunTranslationsService Integration Tests")]
    public class HttpFunTranslationsUnitTests
    {
        /// <summary>
        /// The test prefix
        /// </summary>
        public const string TestPrefix = "HttpFunTranslationsService - ";

        /// <summary>
        /// Determines whether this instance [can do shakespearean translation].
        /// </summary>
        [Fact(DisplayName = TestPrefix + "Can do shakespearean translation")]
        public async Task CanDoShakespeareanTranslation()
        {
            // Arrange

            // Declare a new services collection
            var services = new ServiceCollection();

            // Add logging
            services.AddLogging();

            // Add http client 
            services.AddHttpClient<IFunTranslationsService, HttpFunTranslationsService>();

            // Configure options for http service
            services.Configure<HttpFunTranslationsServiceOptions>(options =>
            {
                options.BaseUrl = "https://api.funtranslations.com";
                options.TranslateEndpoint = "/translate/";
                options.ShakespeareTranslationEndpoint = "shakespeare.json";
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Retrieve FunTranslationsService instance from the service provider
            var service = serviceProvider.GetRequiredService<IFunTranslationsService>();

            // Declare text to translate
            var textToTranslate = "The quick brown fox jumps over the lazy dog";

            // Act
            var translation = await service.TranslateShakespeareAsync(textToTranslate);

            // Assert
            translation.Should().NotBeNull("Translation response should not be null");
            translation.Success.Should().NotBeNull("Translation success object should not be null");
            translation.Success.Total.Should().Be(1,"Translation success count should be 1");
            translation.Contents.Should().NotBeNull("Translation contents object should not be null");
            translation.Contents.Text.Should().Be(textToTranslate, "Response text to translate should be the same as the provided one");
            translation.Contents.Translation.Should().Be("shakespeare", "Response translation should be shakespeare");
            translation.Contents.Translated.Should().Be("The quick brown fox jumps ov'r the distemperate dog", "Translated text should be like the expected one");
        }
    }
}

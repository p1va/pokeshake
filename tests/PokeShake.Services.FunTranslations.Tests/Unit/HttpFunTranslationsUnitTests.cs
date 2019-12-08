using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokeShake.Services.FunTranslations.Contracts;
using System;
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
        /// Determines whether this instance [can do something].
        /// </summary>
        [Fact(DisplayName = TestPrefix + "Can do something")]
        public async Task CanDoSomething()
        {

        }
    }
}

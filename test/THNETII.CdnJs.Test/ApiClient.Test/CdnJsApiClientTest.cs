using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Polly;

using THNETII.Common;

using Xunit;
using Xunit.Abstractions;

namespace THNETII.CdnJs.ApiClient.Test
{
    public static class CdnJsApiClientTest
    {
        [Collection(nameof(CdnJsApiClientCollection))]
        public class BaseTest : IDisposable
        {
            public BaseTest(CdnJsApiClientFixture fixture, ITestOutputHelper outputHelper)
            {
                var services = new ServiceCollection();
                services.AddLogging(logging => logging.AddXUnit(outputHelper));
                services.AddHttpClient<CdnJsApiClient>()
                    .ConfigurePrimaryHttpMessageHandler(serviceProvider =>
                    {
                        var shared = fixture.SharedServices;
                        return shared.GetRequiredService<IHttpMessageHandlerFactory>()
                            .CreateHandler(nameof(CdnJs));
                    })
                    .AddPolicyHandler(Policy
                        .HandleResult<HttpResponseMessage>(msg =>
                        {
                            int statusCode = (int)msg.StatusCode;
                            return statusCode >= 500 && statusCode < 600;
                        })
                        .WaitAndRetryForeverAsync(i => TimeSpan.FromMilliseconds(200))
                    );

                ServiceProvider = services.BuildServiceProvider();
            }

            public ServiceProvider ServiceProvider { get; }

            #region IDisposable
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    ServiceProvider.Dispose();
                }
            }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }

        public static class Constructor
        {
            [Fact]
            public static void ThrowsForNullHttpClient()
            {
                Assert.Throws<ArgumentNullException>(
                    paramName: "httpClient",
                    () => new CdnJsApiClient(httpClient: null!));
            }

            [Fact]
            [SuppressMessage("Reliability", "CA2000: Dispose objects before losing scope")]
            public static async Task TakesDisposableOwnershipOfClient()
            {
                var exampleUri = new Uri("https://example.org/");

                var http = new HttpClient();
                using (var client = new CdnJsApiClient(http)) { }

                await Assert.ThrowsAsync<ObjectDisposedException>(
                    () => http.GetAsync(exampleUri)
                    )
                    .ConfigureAwait(false);
            }
        }

        [SuppressMessage("Naming",
            "CA1724: Type names should not match namespaces",
            Justification = "XUnit")]
        public class DependencyInjection : BaseTest
        {
            public DependencyInjection(CdnJsApiClientFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper) { }

            [Fact]
            public void GetRequiredServiceReturnsNonNull()
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                Assert.NotNull(client);
            }
        }


        public class GetAllLibraries : BaseTest
        {
            public GetAllLibraries(CdnJsApiClientFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper) { }

            [Fact]
            public async Task ThrowsIfCancelledToken()
            {
                CancellationToken cancelToken = new CancellationToken(canceled: true);
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                    client.GetAllLibraries(cancelToken: cancelToken))
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public class GetLibrary : BaseTest
        {
            public static IEnumerable<object[]> GetAllLibraryNames()
            {
                var services = new ServiceCollection();
                services.AddHttpClient<CdnJsApiClient>();
                using var provider = services.BuildServiceProvider();
                using var client = provider.GetRequiredService<CdnJsApiClient>();
                var libraries = client.GetAllLibraries(
                    new CdnJsApiRequestOptions { Fields = { "name" } }
                    ).ConfigureAwait(false).GetAwaiter().GetResult();
                return libraries.Select(l => new[] { l.Name! });
            }

            public GetLibrary(CdnJsApiClientFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper) { }

            [Fact]
            public async Task ThrowsIfCancelledToken()
            {
                const string library = "test";

                CancellationToken cancelToken = new CancellationToken(canceled: true);
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                    client.GetLibrary(library, cancelToken: cancelToken))
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            

            [Fact]
            public async Task ThrowsForNullLibraryName()
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAsync<ArgumentNullException>("name",
                    () => client.GetLibrary(null!)
                    ).ConfigureAwait(continueOnCapturedContext: false);
            }

            [Theory]
            [MemberData(nameof(GetAllLibraryNames))]
            public async Task ReturnsMetadataForLibrary(string name)
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                var library = await client.GetLibrary(name)
                    .ConfigureAwait(continueOnCapturedContext: false);

                Assert.NotNull(library);
                Assert.Equal(name, library.Name);
            }
        }

        public class GetLibraryAssets : BaseTest
        {
            public static IEnumerable<object[]> GetLibraryVersions()
            {
                string[] sampleLibraries = new[]
                {
                    "jquery",
                    "popper.js",
                    "vue",
                    "twitter-bootstrap",
                    "open-iconic"
                };

                var services = new ServiceCollection();
                services.AddHttpClient<CdnJsApiClient>();
                using var provider = services.BuildServiceProvider();

                var libraryVersions = Task
                    .WhenAll(sampleLibraries.Select(async name =>
                    {
                        using var client = provider.GetRequiredService<CdnJsApiClient>();
                        var metadata = await client.GetLibrary(name)
                            .ConfigureAwait(continueOnCapturedContext: false);
                        return (Name: name, Versions: metadata.Versions.ZeroLengthIfNull());
                    }))
                    .ConfigureAwait(continueOnCapturedContext: false)
                    .GetAwaiter().GetResult();

                return libraryVersions.SelectMany(tuple =>
                    tuple.Versions.Select(v => new[] { tuple.Name, v })
                    );
            }

            public GetLibraryAssets(CdnJsApiClientFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper) { }

            [Fact]
            public async Task ThrowsIfCancelledToken()
            {
                const string library = "test";
                const string version = "test";

                CancellationToken cancelToken = new CancellationToken(canceled: true);
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                    client.GetLibraryAssets(library, version, cancelToken: cancelToken))
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            [Fact]
            public async Task ThrowsForNullLibraryName()
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAsync<ArgumentNullException>("name",
                    () => client.GetLibraryAssets(name: null!, version: "test")
                    ).ConfigureAwait(continueOnCapturedContext: false);
            }

            [Fact]
            public async Task ThrowsForNullVersion()
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                await Assert.ThrowsAsync<ArgumentNullException>("version",
                    () => client.GetLibraryAssets(name: "test", version: null!)
                    ).ConfigureAwait(continueOnCapturedContext: false);
            }

            [Theory]
            [MemberData(nameof(GetLibraryVersions))]
            public async Task ReturnsAssetsForLibraryVersion(string name, string version)
            {
                using var client = ServiceProvider.GetRequiredService<CdnJsApiClient>();

                var assets = await client.GetLibraryAssets(name, version)
                    .ConfigureAwait(continueOnCapturedContext: false);

                Assert.NotNull(assets);
                foreach (var fileName in (assets?.Sri?.Keys).EmptyIfNull())
                    Assert.Contains(fileName, (assets?.Files).EmptyIfNull(), StringComparer.OrdinalIgnoreCase);
            }
        }
    }

    [CollectionDefinition(nameof(CdnJsApiClientCollection))]
    public class CdnJsApiClientCollection : ICollectionFixture<CdnJsApiClientFixture> { }

    public class CdnJsApiClientFixture : IDisposable
    {
        public CdnJsApiClientFixture()
        {
            var services = new ServiceCollection();
            services.AddHttpClient(nameof(CdnJs));

            SharedServices = services.BuildServiceProvider();
        }

        public ServiceProvider SharedServices { get; }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SharedServices.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using THNETII.Common;

namespace THNETII.CdnJs.ApiClient
{
    public class CdnJsApiClient : IDisposable
    {
        public static Uri BaseUri { get; } = new Uri("https://api.cdnjs.com/");
        public static JsonSerializerOptions? DefaultSerializerOptions { get; set; }

        private readonly HttpClient httpClient;

        public JsonSerializerOptions? SerializerOptions { get; set; } =
            DefaultSerializerOptions;

        public CdnJsApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.BaseAddress = BaseUri;
        }

        public async Task<CdnJsLibraryMetadata[]?> GetAllLibraries(
            CdnJsApiRequestOptions? options = null,
            CancellationToken cancelToken = default)
        {
            options ??= CdnJsApiRequestOptions.Minimal;

            const string address = "libraries";
            string requUrl = address + options?.ToQueryString();

            var result = await httpClient
                .GetFromJsonAsync<CdnJsLibrarySearchResult?>(
                    requUrl, SerializerOptions, cancelToken)
                .ConfigureAwait(continueOnCapturedContext: false);

            return result?.Results;
        }

        public Task<CdnJsLibraryMetadata[]?> SearchLibrary(
            CdnJsApiSearchRequestOptions options,
            CancellationToken cancelToken = default) =>
            GetAllLibraries(options, cancelToken);

        public Task<CdnJsLibraryMetadata> GetLibrary(string name,
            CdnJsApiRequestOptions? options = null,
            CancellationToken cancelToken = default)
        {
            options ??= CdnJsApiRequestOptions.Minimal;

            _ = name.ThrowIfNullOrEmpty(nameof(name));

            const string address = "libraries";
            string requUrl = address + "/" +
                Uri.EscapeDataString(name) +
                options?.ToQueryString();

            return httpClient.GetFromJsonAsync<CdnJsLibraryMetadata>(
                requUrl, SerializerOptions, cancelToken);
        }

        [SuppressMessage("Design", "CA1062:Validate arguments of public methods",
            Justification = "false negative")]
        public Task<CdnJsLibraryAssets> GetLibraryAssets(string name,
            string version, CdnJsApiRequestOptions? options = null,
            CancellationToken cancelToken = default)
        {
            _ = name.ThrowIfNullOrEmpty(nameof(name));
            _ = version.ThrowIfNullOrEmpty(nameof(version));

            options ??= CdnJsApiRequestOptions.Unspecified;

            const string address = "libraries";
            // Long concatenation, using StringBuilder for performance
            string requUrl = new StringBuilder(address.Length + name.Length + version.Length + 15)
                .Append(address + "/")
                .Append(Uri.EscapeDataString(name)).Append('/')
                .Append(Uri.EscapeDataString(version))
                .Append(options?.ToQueryString())
                .ToString();

            return httpClient.GetFromJsonAsync<CdnJsLibraryAssets>(
                requUrl, SerializerOptions, cancelToken);
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                httpClient.Dispose();
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

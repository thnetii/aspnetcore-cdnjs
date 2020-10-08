using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace THNETII.CdnJs.ApiClient
{
    [SuppressMessage("Performance",
        "CA1819: Properties should not return arrays",
        Justification = "DTO")]
    public class CdnJsLibrarySearchResult
    {
        [JsonPropertyName("results")]
        public CdnJsLibraryMetadata[]? Results { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("available")]
        public int Available { get; set; }
    }
}

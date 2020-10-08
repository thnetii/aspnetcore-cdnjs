using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace THNETII.CdnJs.ApiClient
{
    [SuppressMessage("Performance",
        "CA1819: Properties should not return arrays",
        Justification = "DTO")]
    public class CdnJsLibraryAssets : CdnJsLibraryBaseMetadata
    {
        internal const string PropertyRawFiles = "rawFiles";
        [DataMember(Name = PropertyRawFiles), JsonPropertyName(PropertyRawFiles)]
        public string[]? RawFiles { get; set; }

        internal const string PropertyFiles = "files";
        [DataMember(Name = PropertyFiles), JsonPropertyName(PropertyFiles)]
        public string[]? Files { get; set; }

        internal const string PropertySri = "sri";
        [DataMember(Name = PropertySri), JsonPropertyName(PropertySri)]
        public Dictionary<string, string>? Sri { get; set; }
    }
}

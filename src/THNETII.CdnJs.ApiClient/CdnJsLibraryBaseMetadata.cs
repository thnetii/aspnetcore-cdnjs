using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace THNETII.CdnJs.ApiClient
{
    [SuppressMessage("Usage",
        "CA2227: Collection properties should be read only",
        Justification = "DTO")]
    public abstract class CdnJsLibraryBaseMetadata
    {
        internal const string PropertyName = "name";
        [DataMember(Name = PropertyName), JsonPropertyName(PropertyName)]
        public string? Name { get; set; }

        internal const string PropertyVersion = "version";
        [DataMember(Name = PropertyVersion), JsonPropertyName(PropertyVersion)]
        public string? Version { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? Extensions { get; set; }
    }
}

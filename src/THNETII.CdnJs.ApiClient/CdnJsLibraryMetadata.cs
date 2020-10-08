using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using THNETII.Common;

namespace THNETII.CdnJs.ApiClient
{
    [SuppressMessage("Performance",
        "CA1819: Properties should not return arrays",
        Justification = "DTO")]
    public class CdnJsLibraryMetadata : CdnJsLibraryBaseMetadata
    {
        internal const string PropertyLatestMainFileUrl = "latest";
        private readonly DuplexConversionTuple<string?, Uri?> latestMainFileUri =
            SerializationHelpers.CreateUriConversionTuple();
        [DataMember(Name = PropertyLatestMainFileUrl), JsonPropertyName(PropertyLatestMainFileUrl)]
        public string? LatestMainFileUrl
        {
            get => latestMainFileUri.RawValue;
            set => latestMainFileUri.RawValue = value;
        }
        [IgnoreDataMember, JsonIgnore]
        public Uri? LatestMainFileUri
        {
            get => latestMainFileUri.ConvertedValue;
            set => latestMainFileUri.ConvertedValue = value;
        }

        internal const string PropertyLatestMainFileSri = "sri";
        [DataMember(Name = PropertyLatestMainFileSri), JsonPropertyName(PropertyLatestMainFileSri)]
        public string? LatestMainFileSri { get; set; }

        internal const string PropertyLatestMainFilename = "filename";
        [DataMember(Name = PropertyLatestMainFilename), JsonPropertyName(PropertyLatestMainFilename)]
        public string? LatestMainFilename { get; set; }

        internal const string PropertyDescription = "description";
        [DataMember(Name = PropertyDescription), JsonPropertyName(PropertyDescription)]
        public string? Description { get; set; }

        internal const string PropertyKeywords = "keywords";
        [DataMember(Name = PropertyKeywords), JsonPropertyName(PropertyKeywords)]
        public string[]? Keywords { get; set; }

        internal const string PropertyAuthor = "author";
        [DataMember(Name = PropertyAuthor), JsonPropertyName(PropertyAuthor)]
        public string? Author { get; set; }

        internal const string PropertyHomepageUrl = "homepage";
        private readonly DuplexConversionTuple<string?, Uri?> homepage =
            SerializationHelpers.CreateUriConversionTuple();
        [DataMember(Name = PropertyHomepageUrl), JsonPropertyName(PropertyHomepageUrl)]
        public string? HomepageUrl
        {
            get => homepage.RawValue;
            set => homepage.RawValue = value;
        }
        [IgnoreDataMember, JsonIgnore]
        public Uri? HomepageUri
        {
            get => homepage.ConvertedValue;
            set => homepage.ConvertedValue = value;
        }

        internal const string PropertyAssets = "assets";
        [DataMember(Name = PropertyAssets), JsonPropertyName(PropertyAssets)]
        public CdnJsLibraryAssets[]? Assets { get; set; }

        internal const string PropertyVersions = "versions";
        [DataMember(Name = PropertyVersions), JsonPropertyName(PropertyVersions)]
        public string[]? Versions { get; set; }
    }
}

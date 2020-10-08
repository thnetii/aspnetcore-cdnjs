using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using THNETII.CdnJs.JQuery;

[assembly: AssemblyMetadata(
    JQueryConstants.CdnJsBaseUrlMetadataKey,
    JQueryConstants.CdnJsBaseUrlConst)]
[assembly: AssemblyMetadata(
    JQueryConstants.CdnJsLibraryNameMetadataKey,
    JQueryConstants.CdnJsLibraryNameConst)]

[assembly: AssemblyMetadata(
    JQueryConstants.Source.NameMetadataKey,
    JQueryConstants.Source.NameConst)]
[assembly: AssemblyMetadata(
    JQueryConstants.Source.SriMetadataKey,
    JQueryConstants.Source.SriConst)]

[assembly: AssemblyMetadata(
    JQueryConstants.Minified.NameMetadataKey,
    JQueryConstants.Minified.NameConst)]
[assembly: AssemblyMetadata(
    JQueryConstants.Minified.SriMetadataKey,
    JQueryConstants.Minified.SriConst)]

[assembly: AssemblyMetadata(
    JQueryConstants.Slim.NameMetadataKey,
    JQueryConstants.Slim.NameConst)]
[assembly: AssemblyMetadata(
    JQueryConstants.Slim.SriMetadataKey,
    JQueryConstants.Slim.SriConst)]

[assembly: AssemblyMetadata(
    JQueryConstants.SlimMinified.NameMetadataKey,
    JQueryConstants.SlimMinified.NameConst)]
[assembly: AssemblyMetadata(
    JQueryConstants.SlimMinified.SriMetadataKey,
    JQueryConstants.SlimMinified.SriConst)]

namespace THNETII.CdnJs.JQuery
{
    [SuppressMessage("Design", "CA1034: Nested types should not be visible",
        Justification = "Grouped Constants")]
    [SuppressMessage("Design", "CA1056: Uri properties should not be strings",
        Justification = "Need as string")]
    public static class JQueryConstants
    {
        internal const string CdnJsLibraryNameConst = "jquery";
        private const string AspFallbackTestConst = "window.jQuery";
        internal const string CdnJsBaseUrlConst =
            "https://cdnjs.cloudflare.com/ajax/libs/" + CdnJsLibraryNameConst;

        public static AssemblyName AssemblyName { get; } =
            typeof(JQueryConstants).Assembly.GetName();
        public static string Version { get; } = typeof(JQueryConstants)
            .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion?.Split(new[] { '+' }, 2)[0] ??
            typeof(JQueryConstants).Assembly.GetName().Version
            .ToString(3);

        internal const string CdnJsLibraryNameMetadataKey =
            nameof(JQuery) + nameof(CdnJsLibraryName);
        public static string CdnJsLibraryName { get; } = CdnJsLibraryNameConst;

        internal const string CdnJsBaseUrlMetadataKey =
            nameof(JQuery) + nameof(CdnJsBaseUrl);
        
        public static string CdnJsBaseUrl { get; } = CdnJsBaseUrlConst;
        public static string CdnJsRootUrl { get; } =
            FormattableString.Invariant($"{CdnJsBaseUrl}/{Version}");

        public static string LocalBaseUrl { get; } =
            FormattableString.Invariant($"_content/{AssemblyName.Name}/lib/{CdnJsLibraryName}");

        public static string AspFallbackTest { get; } = AspFallbackTestConst;

        public static class Source
        {
            internal const string NameConst = "jquery.js";
            internal const string SriConst =
                "sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=";

            private const string MetadataPrefix =
                nameof(JQuery) + nameof(Source);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class Minified
        {
            internal const string NameConst = "jquery.min.js";
            internal const string SriConst =
                "sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=";

            private const string MetadataPrefix =
                nameof(JQuery) + nameof(Minified);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class Slim
        {
            internal const string NameConst = "jquery.slim.js";
            internal const string SriConst =
                "sha256-DrT5NfxfbHvMHux31Lkhxg42LY6of8TaYyK50jnxRnM=";

            private const string MetadataPrefix =
                nameof(JQuery) + nameof(Slim);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class SlimMinified
        {
            internal const string NameConst = "jquery.slim.min.js";
            internal const string SriConst =
                "sha256-4+XzXVhsDmqanXGHaHvgh1gMQKX40OUvDEBTu8JcmNs=";

            private const string MetadataPrefix =
                nameof(JQuery) + nameof(SlimMinified);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }
    }
}

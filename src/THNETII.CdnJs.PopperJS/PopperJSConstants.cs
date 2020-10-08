using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using THNETII.CdnJs.PopperJS;

[assembly: AssemblyMetadata(
    PopperJSConstants.CdnJsBaseUrlMetadataKey,
    PopperJSConstants.CdnJsBaseUrlConst)]
[assembly: AssemblyMetadata(
    PopperJSConstants.CdnJsLibraryNameMetadataKey,
    PopperJSConstants.CdnJsLibraryNameConst)]

[assembly: AssemblyMetadata(
    PopperJSConstants.Source.NameMetadataKey,
    PopperJSConstants.Source.NameConst)]
[assembly: AssemblyMetadata(
    PopperJSConstants.Source.SriMetadataKey,
    PopperJSConstants.Source.SriConst)]

[assembly: AssemblyMetadata(
    PopperJSConstants.Minified.NameMetadataKey,
    PopperJSConstants.Minified.NameConst)]
[assembly: AssemblyMetadata(
    PopperJSConstants.Minified.SriMetadataKey,
    PopperJSConstants.Minified.SriConst)]

[assembly: AssemblyMetadata(
    PopperJSConstants.Lite.NameMetadataKey,
    PopperJSConstants.Lite.NameConst)]
[assembly: AssemblyMetadata(
    PopperJSConstants.Lite.SriMetadataKey,
    PopperJSConstants.Lite.SriConst)]

[assembly: AssemblyMetadata(
    PopperJSConstants.LiteMinified.NameMetadataKey,
    PopperJSConstants.LiteMinified.NameConst)]
[assembly: AssemblyMetadata(
    PopperJSConstants.LiteMinified.SriMetadataKey,
    PopperJSConstants.LiteMinified.SriConst)]

namespace THNETII.CdnJs.PopperJS
{
    [SuppressMessage("Design", "CA1034: Nested types should not be visible",
        Justification = "Grouped Constants")]
    [SuppressMessage("Design", "CA1056: Uri properties should not be strings",
        Justification = "Need as string")]
    public static class PopperJSConstants
    {
        internal const string CdnJsLibraryNameConst = "popper.js";
        private const string AspFallbackTestConst = "window.Popper";
        internal const string CdnJsBaseUrlConst =
            "https://cdnjs.cloudflare.com/ajax/libs/" + CdnJsLibraryNameConst;

        public static AssemblyName AssemblyName { get; } =
            typeof(PopperJSConstants).Assembly.GetName();
        public static string Version { get; } = typeof(PopperJSConstants)
            .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion?.Split(new[] { '+' }, 2)[0] ??
            typeof(PopperJSConstants).Assembly.GetName().Version
            .ToString(3);

        internal const string CdnJsLibraryNameMetadataKey =
            nameof(PopperJS) + nameof(CdnJsLibraryName);
        public static string CdnJsLibraryName { get; } = CdnJsLibraryNameConst;

        internal const string CdnJsBaseUrlMetadataKey =
            nameof(PopperJS) + nameof(CdnJsBaseUrl);
        public static string CdnJsBaseUrl { get; } = CdnJsBaseUrlConst;
        public static string CdnJsRootUrl { get; } =
            FormattableString.Invariant($"{CdnJsBaseUrl}/{Version}");

        public static string LocalBaseUrl { get; } =
            FormattableString.Invariant($"_content/{AssemblyName.Name}/lib/{CdnJsLibraryName}");

        public static string AspFallbackTest { get; } = AspFallbackTestConst;

        public static class Source
        {
            internal const string NameConst = "umd/popper.js";
            internal const string SriConst =
                "sha256-c19z0qoUHRAEAfVnlZCHbzLyBk46F379Q+h+C2n2xi8=";

            private const string MetadataPrefix =
                nameof(PopperJS) + nameof(Source);
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
            internal const string NameConst = "umd/popper.min.js";
            internal const string SriConst =
                "sha256-XahKYIZhnEztrOcCTmaEErjYDLoLqBoDJbVMYybyjH8=";

            private const string MetadataPrefix =
                nameof(PopperJS) + nameof(Minified);
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

        public static class Lite
        {
            internal const string NameConst = "umd/popper-lite.js";
            internal const string SriConst =
                "sha256-DrT5NfxfbHvMHux31Lkhxg42LY6of8TaYyK50jnxRnM=";

            private const string MetadataPrefix =
                nameof(PopperJS) + nameof(Lite);
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

        public static class LiteMinified
        {
            internal const string NameConst = "umd/popper-lite.min.js";
            internal const string SriConst =
                "sha256-4+XzXVhsDmqanXGHaHvgh1gMQKX40OUvDEBTu8JcmNs=";

            private const string MetadataPrefix =
                nameof(PopperJS) + nameof(LiteMinified);
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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using THNETII.CdnJs.Bootstrap;

[assembly: AssemblyMetadata(
    BootstrapConstants.CdnJsBaseUrlMetadataKey,
    BootstrapConstants.CdnJsBaseUrlConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.CdnJsLibraryNameMetadataKey,
    BootstrapConstants.CdnJsLibraryNameConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.JavaScript.NameMetadataKey,
    BootstrapConstants.JavaScript.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.JavaScript.SriMetadataKey,
    BootstrapConstants.JavaScript.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.JavaScriptMinified.NameMetadataKey,
    BootstrapConstants.JavaScriptMinified.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.JavaScriptMinified.SriMetadataKey,
    BootstrapConstants.JavaScriptMinified.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.StyleSheet.NameMetadataKey,
    BootstrapConstants.StyleSheet.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.StyleSheet.SriMetadataKey,
    BootstrapConstants.StyleSheet.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.StyleSheetMinified.NameMetadataKey,
    BootstrapConstants.StyleSheetMinified.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.StyleSheetMinified.SriMetadataKey,
    BootstrapConstants.StyleSheetMinified.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.GridStyleSheet.NameMetadataKey,
    BootstrapConstants.GridStyleSheet.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.GridStyleSheet.SriMetadataKey,
    BootstrapConstants.GridStyleSheet.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.GridStyleSheetMinified.NameMetadataKey,
    BootstrapConstants.GridStyleSheetMinified.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.GridStyleSheetMinified.SriMetadataKey,
    BootstrapConstants.GridStyleSheetMinified.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.RebootStyleSheet.NameMetadataKey,
    BootstrapConstants.RebootStyleSheet.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.RebootStyleSheet.SriMetadataKey,
    BootstrapConstants.RebootStyleSheet.SriConst)]

[assembly: AssemblyMetadata(
    BootstrapConstants.RebootStyleSheetMinified.NameMetadataKey,
    BootstrapConstants.RebootStyleSheetMinified.NameConst)]
[assembly: AssemblyMetadata(
    BootstrapConstants.RebootStyleSheetMinified.SriMetadataKey,
    BootstrapConstants.RebootStyleSheetMinified.SriConst)]

namespace THNETII.CdnJs.Bootstrap
{
    [SuppressMessage("Design", "CA1034: Nested types should not be visible",
        Justification = "Grouped Constants")]
    [SuppressMessage("Design", "CA1056: Uri properties should not be strings",
        Justification = "Need as string")]
    public static class BootstrapConstants
    {
        internal const string CdnJsLibraryNameConst = "twitter-bootstrap";
        internal const string CdnJsBaseUrlConst =
            "https://cdnjs.cloudflare.com/ajax/libs/" + CdnJsLibraryNameConst;

        public static AssemblyName AssemblyName { get; } =
            typeof(BootstrapConstants).Assembly.GetName();
        public static string Version { get; } = typeof(BootstrapConstants)
            .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion?.Split(new[] { '+' }, 2)[0] ??
            typeof(BootstrapConstants).Assembly.GetName().Version
            .ToString(3);

        internal const string CdnJsLibraryNameMetadataKey =
            nameof(Bootstrap) + nameof(CdnJsLibraryName);
        public static string CdnJsLibraryName { get; } = CdnJsLibraryNameConst;

        internal const string CdnJsBaseUrlMetadataKey =
            nameof(Bootstrap) + nameof(CdnJsBaseUrl);

        public static string CdnJsBaseUrl { get; } = CdnJsBaseUrlConst;
        public static string CdnJsRootUrl { get; } =
            FormattableString.Invariant($"{CdnJsBaseUrl}/{Version}");

        public static string LocalBaseUrl { get; } =
            FormattableString.Invariant($"_content/{AssemblyName.Name}/lib/{CdnJsLibraryName}");


        public static class JavaScript
        {
            private const string AspFallbackTestConst = "window.bootstrap";
            internal const string NameConst = "js/bootstrap.js";
            internal const string SriConst =
                "sha256-i/Jq6Tc8SbPMBrnvq/sOTfH81hW5emVa4OzZPqhcwtI=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(JavaScript);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string AspFallbackTest { get; } = AspFallbackTestConst;

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class JavaScriptMinified
        {
            internal const string NameConst = "js/bootstrap.min.js";
            internal const string SriConst =
                "sha256-OFRAJNoaD8L3Br5lglV7VyLRf0itmoBzWUoM+Sji4/8=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(JavaScriptMinified);
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

        public static class StyleSheet
        {
            private const string AspFallbackTestClassConst = "sr-only";
            private const string AspFallbackTestPropertyConst = "position";
            private const string AspFallbackTestValueConst = "absolute";
            internal const string NameConst = "css/bootstrap.css";
            internal const string SriConst =
                "sha256-1hm7xPFY4HL/GPfWz595kcNLVmuMC43nPagoQhWTb58=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(StyleSheet);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string AspFallbackTestClass { get; } =
                AspFallbackTestClassConst;
            public static string AspFallbackTestProperty { get; } =
                AspFallbackTestPropertyConst;
            public static string AspFallbackTestValue { get; } =
                AspFallbackTestValueConst;

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class StyleSheetMinified
        {
            internal const string NameConst = "css/bootstrap.min.css";
            internal const string SriConst =
                "sha256-aAr2Zpq8MZ+YA/D6JtRD3xtrwpEz2IqOS+pWD/7XKIw=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(StyleSheetMinified);
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

        public static class RebootStyleSheet
        {
            private const string AspFallbackTestClassConst = "template";
            private const string AspFallbackTestPropertyConst = "display";
            private const string AspFallbackTestValueConst = "none";
            internal const string NameConst = "css/bootstrap-reboot.css";
            internal const string SriConst =
                "sha256-51QrAVIhLXt+SoOxWeVXN1prxWamwam/xojOiZ9kV9M=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(RebootStyleSheet);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string AspFallbackTestClass { get; } =
                AspFallbackTestClassConst;
            public static string AspFallbackTestProperty { get; } =
                AspFallbackTestPropertyConst;
            public static string AspFallbackTestValue { get; } =
                AspFallbackTestValueConst;

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class RebootStyleSheetMinified
        {
            internal const string NameConst = "css/bootstrap-reboot.min.css";
            internal const string SriConst =
                "sha256-xYVniYXUBtVTE4ja+KwHMJju/nGqmLCOJxoKGhnkspU=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(RebootStyleSheetMinified);
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

        public static class GridStyleSheet
        {
            private const string AspFallbackTestClassConst = "row";
            private const string AspFallbackTestPropertyConst = "display";
            private const string AspFallbackTestValueConst = "flex";
            internal const string NameConst = "css/bootstrap-grid.css";
            internal const string SriConst =
                "sha256-PM4GO42KoMjqwoXyG9KN7R68yxVdOLsFbTbV6T970+k=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(GridStyleSheet);
            internal const string NameMetadataKey =
                MetadataPrefix + nameof(Name);
            internal const string SriMetadataKey =
                MetadataPrefix + nameof(Sri);

            public static string AspFallbackTestClass { get; } =
                AspFallbackTestClassConst;
            public static string AspFallbackTestProperty { get; } =
                AspFallbackTestPropertyConst;
            public static string AspFallbackTestValue { get; } =
                AspFallbackTestValueConst;

            public static string Name { get; } = NameConst;
            public static string Url { get; } =
                FormattableString.Invariant($"{CdnJsRootUrl}/{Name}");
            public static string LocalUrl { get; } =
                FormattableString.Invariant($"{LocalBaseUrl}/{Name}");
            public static string Sri { get; } = SriConst;
        }

        public static class GridStyleSheetMinified
        {
            internal const string NameConst = "css/bootstrap-grid.min.css";
            internal const string SriConst =
                "sha256-4hb0ms2+lEuANNAyOsA8sVYq2208vHZ76+gepW4dpgM=";

            private const string MetadataPrefix =
                nameof(Bootstrap) + nameof(GridStyleSheetMinified);
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

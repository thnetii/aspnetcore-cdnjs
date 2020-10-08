using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using THNETII.CdnJs.JQuery.Validate;

[assembly: AssemblyMetadata(
    JQueryValidateConstants.CdnJsBaseUrlMetadataKey,
    JQueryValidateConstants.CdnJsBaseUrlConst)]
[assembly: AssemblyMetadata(
    JQueryValidateConstants.CdnJsLibraryNameMetadataKey,
    JQueryValidateConstants.CdnJsLibraryNameConst)]

[assembly: AssemblyMetadata(
    JQueryValidateConstants.Source.NameMetadataKey,
    JQueryValidateConstants.Source.NameConst)]
[assembly: AssemblyMetadata(
    JQueryValidateConstants.Source.SriMetadataKey,
    JQueryValidateConstants.Source.SriConst)]

[assembly: AssemblyMetadata(
    JQueryValidateConstants.Minified.NameMetadataKey,
    JQueryValidateConstants.Minified.NameConst)]
[assembly: AssemblyMetadata(
    JQueryValidateConstants.Minified.SriMetadataKey,
    JQueryValidateConstants.Minified.SriConst)]

namespace THNETII.CdnJs.JQuery.Validate
{
    [SuppressMessage("Design", "CA1034: Nested types should not be visible",
        Justification = "Grouped Constants")]
    [SuppressMessage("Design", "CA1056: Uri properties should not be strings",
        Justification = "Need as string")]
    public static class JQueryValidateConstants
    {
        internal const string CdnJsLibraryNameConst = "jquery-validate";
        private const string AspFallbackTestConst = "window.jQuery && window.jQuery.validator";
        internal const string CdnJsBaseUrlConst =
            "https://cdnjs.cloudflare.com/ajax/libs/" + CdnJsLibraryNameConst;

        public static AssemblyName AssemblyName { get; } =
            typeof(JQueryValidateConstants).Assembly.GetName();
        public static string Version { get; } = typeof(JQueryValidateConstants)
            .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion?.Split(new[] { '+' }, 2)[0] ??
            typeof(JQueryValidateConstants).Assembly.GetName().Version?
            .ToString(3) ?? string.Empty;
        private const string MetadataPrefix = nameof(JQuery) + "Validate";

        internal const string CdnJsLibraryNameMetadataKey =
            MetadataPrefix + nameof(CdnJsLibraryName);
        public static string CdnJsLibraryName { get; } = CdnJsLibraryNameConst;

        internal const string CdnJsBaseUrlMetadataKey =
            MetadataPrefix + nameof(CdnJsBaseUrl);
        
        public static string CdnJsBaseUrl { get; } = CdnJsBaseUrlConst;
        public static string CdnJsRootUrl { get; } =
            FormattableString.Invariant($"{CdnJsBaseUrl}/{Version}");

        public static string LocalBaseUrl { get; } =
            FormattableString.Invariant($"_content/{AssemblyName.Name}/lib/{CdnJsLibraryName}");

        public static string AspFallbackTest { get; } = AspFallbackTestConst;

        public static class Source
        {
            internal const string NameConst = "jquery.validate.js";
            internal const string SriConst =
                "sha256-5TEmw9l5YdbVgo3xss1VI3Aic2WAxd6ndG5kOSwxUBk=";

            private const string MetadataPrefix =
                JQueryValidateConstants.MetadataPrefix + nameof(Source);
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
            internal const string NameConst = "jquery.validate.min.js";
            internal const string SriConst =
                "sha256-+BEKmIvQ6IsL8sHcvidtDrNOdZO3C9LtFPtF2H0dOHI=";

            private const string MetadataPrefix =
                JQueryValidateConstants.MetadataPrefix + nameof(Minified);
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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using THNETII.CdnJs.JQuery.Validation.Unobtrusive;

[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.CdnJsBaseUrlMetadataKey,
    JQueryValidationUnobtrusiveConstants.CdnJsBaseUrlConst)]
[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.CdnJsLibraryNameMetadataKey,
    JQueryValidationUnobtrusiveConstants.CdnJsLibraryNameConst)]

[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.Source.NameMetadataKey,
    JQueryValidationUnobtrusiveConstants.Source.NameConst)]
[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.Source.SriMetadataKey,
    JQueryValidationUnobtrusiveConstants.Source.SriConst)]

[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.Minified.NameMetadataKey,
    JQueryValidationUnobtrusiveConstants.Minified.NameConst)]
[assembly: AssemblyMetadata(
    JQueryValidationUnobtrusiveConstants.Minified.SriMetadataKey,
    JQueryValidationUnobtrusiveConstants.Minified.SriConst)]

namespace THNETII.CdnJs.JQuery.Validation.Unobtrusive
{
    [SuppressMessage("Design", "CA1034: Nested types should not be visible",
        Justification = "Grouped Constants")]
    [SuppressMessage("Design", "CA1056: Uri properties should not be strings",
        Justification = "Need as string")]
    public static class JQueryValidationUnobtrusiveConstants
    {
        internal const string CdnJsLibraryNameConst = "jquery-validation-unobtrusive";
        private const string AspFallbackTestConst = "window.jQuery && window.jQuery.validator && window.jQuery.validator.unobtrusive";
        internal const string CdnJsBaseUrlConst =
            "https://cdnjs.cloudflare.com/ajax/libs/" + CdnJsLibraryNameConst;

        public static AssemblyName AssemblyName { get; } =
            typeof(JQueryValidationUnobtrusiveConstants).Assembly.GetName();
        public static string Version { get; } = typeof(JQueryValidationUnobtrusiveConstants)
            .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion?.Split(new[] { '+' }, 2)[0] ??
            typeof(JQueryValidationUnobtrusiveConstants).Assembly.GetName().Version
            .ToString(3);
        private const string MetadataPrefix = nameof(JQuery) + "ValidationUnobtrusive";

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
            internal const string NameConst = "jquery.validate.unobtrusive.js";
            internal const string SriConst =
                "sha256-XNNC8ESw29iopRLukVRazlP44TxnjGmEQanHJ5kHmtk=";

            private const string MetadataPrefix =
                JQueryValidationUnobtrusiveConstants.MetadataPrefix + nameof(Source);
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
            internal const string NameConst = "jquery.validate.unobtrusive.min.js";
            internal const string SriConst =
                "sha256-9GycpJnliUjJDVDqP0UEu/bsm9U+3dnQUH8+3W10vkY=";

            private const string MetadataPrefix =
                JQueryValidationUnobtrusiveConstants.MetadataPrefix + nameof(Minified);
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

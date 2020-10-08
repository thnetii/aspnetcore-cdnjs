using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace THNETII.CdnJs.ApiClient
{
    internal static class CdnJsApiRequestOptionsQueryBuilder
    {
        private static StringBuilder? _queryBuilder = null;

        internal static StringBuilder GetStringBuilder()
        {
            return Interlocked.Exchange(
                ref _queryBuilder,
                null) ?? new StringBuilder();
        }

        internal static void ReturnStringBuilder(StringBuilder queryBuilder)
        {
            queryBuilder.Clear();
            queryBuilder.Capacity = Math.Min(queryBuilder.Capacity, 128);
            _queryBuilder = queryBuilder;
        }
    }

    [DebuggerDisplay("{" + nameof(ToQueryString) + "()}")]
    public class CdnJsApiRequestOptions
    {
        private class Immutable : CdnJsApiRequestOptions
        {
            private readonly bool humanReadable;

            public Immutable(IEnumerable<string> fields, bool humanReadable = false)
                : base(fields.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase))
            {
                this.humanReadable = humanReadable;
            }

            [SuppressMessage("Globalization", "CA1303: Do not pass literals as localized parameters")]
            public override bool HumanReadableOutput
            {
                get => humanReadable;
                set => throw new InvalidOperationException("Immutable options instance cannot be modified after construction");
            }
        }

        private const BindingFlags propBinding =
            BindingFlags.Instance | BindingFlags.Public;
        private static readonly string[] minimalFields = new[]
        {
            nameof(CdnJsLibraryBaseMetadata.Name),
            nameof(CdnJsLibraryBaseMetadata.Version),
            nameof(CdnJsLibraryMetadata.Versions),
        };

        public static CdnJsApiRequestOptions Unspecified { get; } =
            new Immutable(Enumerable.Empty<string>());
        public static CdnJsApiRequestOptions Minimal { get; } =
            new Immutable(minimalFields);
        public static ImmutableDictionary<string, string> JsonPropertyNames { get; } =
            typeof(CdnJsLibraryMetadata)
            .GetProperties(propBinding)
            .Concat(
                typeof(CdnJsLibraryAssets)
                .GetProperties(propBinding)
                )
            .Select(pi => (
                PropertyName: pi.Name,
                JsonName: pi.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
                )
            )
            .Where(prop => !string.IsNullOrEmpty(prop.JsonName))
            .Cast<(string PropertyName, string JsonName)>()
            .ToLookup(prop => prop.PropertyName, prop => prop.JsonName, StringComparer.OrdinalIgnoreCase)
            .ToImmutableDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        public static CdnJsApiRequestOptions All { get; } =
            new Immutable(JsonPropertyNames.Values);

        protected CdnJsApiRequestOptions(ISet<string> fields) : base() =>
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));

        [DebuggerStepThrough]
        public CdnJsApiRequestOptions() : this(new HashSet<string>(
#if !NETSTANDARD2_0
            capacity: JsonPropertyNames.Count,
#endif
            comparer: StringComparer.OrdinalIgnoreCase))
        { }

        public ISet<string> Fields { get; }

        public virtual bool HumanReadableOutput { get; set; }

        public virtual string ToQueryString()
        {
            var queryBuilder =
                CdnJsApiRequestOptionsQueryBuilder.GetStringBuilder();

            bool hasQuery = false;
            string result;
            try
            {
                string fields = string.Join(",", Fields
                    .Select(propName => JsonPropertyNames
                        .TryGetValue(propName, out string jsonName)
                        ? jsonName : propName
                    ));

                if (!string.IsNullOrEmpty(fields))
                {
                    StartQueryParameter(queryBuilder, ref hasQuery);
                    queryBuilder.Append("fields=").Append(fields);
                }

                if (HumanReadableOutput)
                {
                    StartQueryParameter(queryBuilder, ref hasQuery);
                    queryBuilder.Append("output=human");
                }
            }
            finally
            {
                result = hasQuery ? queryBuilder.ToString() : string.Empty;
                CdnJsApiRequestOptionsQueryBuilder.ReturnStringBuilder(queryBuilder);
            }

            return result;
        }

        internal static void StartQueryParameter(StringBuilder sb, ref bool notFirst)
        {
            if (notFirst)
                sb.Append("&");
            else
            {
                sb.Append("?");
                notFirst = true;
            }
        }
    }
}

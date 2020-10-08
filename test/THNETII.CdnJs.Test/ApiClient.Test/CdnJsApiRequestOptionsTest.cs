using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Web;

using Xunit;

namespace THNETII.CdnJs.ApiClient.Test
{
    public static class CdnJsApiRequestOptionsTest
    {
        public static class PropertyFields
        {
            public static IEnumerable<string> GetFieldNames() =>
                CdnJsApiRequestOptions.JsonPropertyNames.Keys;

            public static IEnumerable<string> GetJsonNames() =>
                CdnJsApiRequestOptions.JsonPropertyNames.Values;

            public static IEnumerable<object[]> GetFieldNamesMemberData() =>
                GetFieldNames().Select(n => new[] { n });

            public static IEnumerable<object[]> GetJsonNamesMemberData() =>
                GetJsonNames().Select(n => new[] { n });

            public static IEnumerable<object[]> GetValidNamesMemberData() =>
                GetFieldNamesMemberData().Concat(GetJsonNamesMemberData());

            [Fact]
            public static void IsEmptyInNewInstance()
            {
                var opts = new CdnJsApiRequestOptions();

                Assert.Empty(opts.Fields);
            }

            [Fact]
            public static void NoFieldsQueryParameterIfEmpty()
            {
                var opts = new CdnJsApiRequestOptions()
                { Fields = { } };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.DoesNotContain("fields", param.Keys.Cast<string>());
            }

            [Theory]
            [MemberData(nameof(GetFieldNamesMemberData))]
            public static void FieldsQueryParameterIfContainsPropertyName(string property)
            {
                var opts = new CdnJsApiRequestOptions()
                { Fields = { property } };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Contains("fields", param.Keys.Cast<string>());
                string value = param["fields"];
                Assert.NotEmpty(value);
            }

            [Theory]
            [MemberData(nameof(GetJsonNamesMemberData))]
            public static void FieldsQueryParameterIfContainsJsonName(string json)
            {
                var opts = new CdnJsApiRequestOptions()
                { Fields = { json } };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Contains("fields", param.Keys.Cast<string>());
                string value = param["fields"];
                Assert.Equal(json, value, StringComparer.OrdinalIgnoreCase);
            }

            [Fact]
            public static void MultipleFieldsSeparatedByCommaInQueryParameter()
            {
                var opts = new CdnJsApiRequestOptions()
                {
                    Fields =
                    {
                        nameof(CdnJsLibraryMetadata.Name),
                        nameof(CdnJsLibraryMetadata.Versions)
                    }
                };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Contains("fields", param.Keys.Cast<string>());
                string value = param["fields"];
                Assert.NotEmpty(value);
                string[] items = value.Split(',');
                Assert.Equal(2, items.Length);
            }
        }

        public static class PropertyHumanReadableOutput
        {
            [Fact]
            public static void IsFalseInNewInstance()
            {
                var opts = new CdnJsApiRequestOptions();

                Assert.False(opts.HumanReadableOutput);
            }

            [Fact]
            public static void NoOutputQueryParameterIfFalse()
            {
                var opts = new CdnJsApiRequestOptions()
                { HumanReadableOutput = false };

                string query = opts.ToQueryString();

                Assert.DoesNotContain("output=", query, StringComparison.OrdinalIgnoreCase);
            }

            [Fact]
            public static void OutputQueryParameterIfTrue()
            {
                var opts = new CdnJsApiRequestOptions()
                { HumanReadableOutput = true };

                string query = opts.ToQueryString();

                Assert.Contains("output=", query, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static class MethodToQueryString
        {
            [Fact]
            public static void ReturnsEmptyStringForNewInstance()
            {
                var opts = new CdnJsApiRequestOptions();

                var query = opts.ToQueryString();

                Assert.Equal(string.Empty, query, StringComparer.Ordinal);
            }

            [Fact]
            public static void SupportsMultipleQueryParameters()
            {
                var opts = new CdnJsApiRequestOptions
                {
                    HumanReadableOutput = true,
                    Fields = { nameof(CdnJsLibraryMetadata.Name) }
                };

                var query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Equal(2, param.Count);
            }
        }
    }
}

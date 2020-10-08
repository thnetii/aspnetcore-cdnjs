using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Xunit;

namespace THNETII.CdnJs.ApiClient.Test
{
    public static class CdnJsApiSearchRequestOptionsTest
    {
        public static class PropertySearch
        {
            [Fact]
            public static void NoSearchQueryParameterIfNull()
            {
                var opts = new CdnJsApiSearchRequestOptions("")
                { Search = null! };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.DoesNotContain("search", param.Keys.Cast<string>());
            }

            [Fact]
            public static void SearchQueryParameterIfSet()
            {
                const string test = "test";
                var opts = new CdnJsApiSearchRequestOptions(test);

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Contains("search", param.Keys.Cast<string>());
                string value = param["search"];
                Assert.Equal(test, value, StringComparer.Ordinal);
            }
        }

        public static class MethodToQueryString
        {
            [Fact]
            public static void AddsToBaseClassQueryString()
            {
                const string test = "test";
                var opts = new CdnJsApiSearchRequestOptions(test)
                {
                    HumanReadableOutput = true
                };

                string query = opts.ToQueryString();
                var param = HttpUtility.ParseQueryString(query);

                Assert.Contains("search", param.Keys.Cast<string>());
                Assert.InRange(param.Count, 2, int.MaxValue);
            }
        }
    }
}

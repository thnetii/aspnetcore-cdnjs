using System;

namespace THNETII.CdnJs.ApiClient
{
    public class CdnJsApiSearchRequestOptions : CdnJsApiRequestOptions
    {
        public string Search { get; set; }

        public CdnJsApiSearchRequestOptions(string search)
        {
            Search = search ?? throw new ArgumentNullException(nameof(search));
        }

        public override string ToQueryString()
        {
            string result = base.ToQueryString();

            bool hasQuery = !string.IsNullOrEmpty(result);
            var queryBuilder = CdnJsApiRequestOptionsQueryBuilder
                .GetStringBuilder();
            try
            {
                if (Search is string search)
                {
                    StartQueryParameter(queryBuilder, ref hasQuery);
                    queryBuilder.Append(nameof(search) + "=")
                        .Append(Uri.EscapeDataString(search));
                }
            }
            finally
            {
                result += queryBuilder.Length > 0 ? queryBuilder.ToString() : string.Empty;
                CdnJsApiRequestOptionsQueryBuilder.ReturnStringBuilder(queryBuilder);
            }

            return result;
        }
    }
}

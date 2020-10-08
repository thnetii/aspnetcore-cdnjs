using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace THNETII.CdnJs
{
    public static class JQueryValidationUnobtrusiveMvcExtensions
    {
        public static IMvcBuilder AddJQueryValidationUnobtrusiveApplicationPart(this IMvcBuilder mvc)
            => (mvc ?? throw new ArgumentNullException(nameof(mvc)))
                .AddApplicationPart(typeof(JQueryValidationUnobtrusiveMvcExtensions).Assembly);

        public static async Task<IHtmlContent> JQueryValidationUnobtrusiveScripts(
            this IHtmlHelper html, bool includeDependencies = false)
        {
            _ = html ?? throw new ArgumentNullException(nameof(html));

            var contentBuilder = new HtmlContentBuilder();
            if (includeDependencies)
            {
                contentBuilder.AppendHtml(await html
                    .JQueryValidateScripts(includeDependencies)
                    .ConfigureAwait(false));
            }
            contentBuilder.AppendHtml(await html
                .PartialAsync("/Views/Shared/_JQueryValidationUnobtrusiveScripts.cshtml")
                .ConfigureAwait(false));

            return contentBuilder;
        }
    }
}

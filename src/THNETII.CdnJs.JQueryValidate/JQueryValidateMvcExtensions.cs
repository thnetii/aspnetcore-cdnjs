using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace THNETII.CdnJs
{
    public static class JQueryValidateMvcExtensions
    {
        public static IMvcBuilder AddJQueryValidateApplicationPart(this IMvcBuilder mvc)
            => (mvc ?? throw new ArgumentNullException(nameof(mvc)))
                .AddApplicationPart(typeof(JQueryValidateMvcExtensions).Assembly);

        public static async Task<IHtmlContent?> JQueryValidateScripts(
            this IHtmlHelper html, bool includeDependencies = false)
        {
            _ = html ?? throw new ArgumentNullException(nameof(html));

            var contentBuilder = new HtmlContentBuilder();
            if (includeDependencies)
            {
                contentBuilder.AppendHtml(await html
                    .JQueryScripts()
                    .ConfigureAwait(false));
            }
            contentBuilder.AppendHtml(await html
                .PartialAsync("/Views/Shared/_JQueryValidateScripts.cshtml")
                .ConfigureAwait(false));
            return contentBuilder;
        }
    }
}

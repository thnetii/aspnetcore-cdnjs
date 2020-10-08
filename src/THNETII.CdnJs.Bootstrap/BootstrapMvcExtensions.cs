using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace THNETII.CdnJs
{
    public static class BootstrapMvcExtensions
    {
        public static IMvcBuilder AddBootstrapApplicationPart(
            this IMvcBuilder mvc, bool excludeDependencies = false)
        {
            _ = mvc ?? throw new ArgumentNullException(nameof(mvc));
            if (!excludeDependencies)
            {
                mvc.AddJQueryApplicationPart();
                mvc.AddPopperJSApplicationPart();
            }

            mvc.AddApplicationPart(typeof(BootstrapMvcExtensions).Assembly);

            return mvc;
        }

        public static async Task<IHtmlContent> BootstrapJs(
            this IHtmlHelper html, bool includeDependencies = false)
        {
            _ = html ?? throw new ArgumentNullException(nameof(html));

            var contentBuilder = new HtmlContentBuilder();
            if (includeDependencies)
            {
                contentBuilder.AppendHtml(await html
                    .JQueryScripts()
                    .ConfigureAwait(false));
                contentBuilder.AppendHtml(await html
                    .PopperJsUmdScripts()
                    .ConfigureAwait(false));
            }
            contentBuilder.AppendHtml(await html
                .PartialAsync("/Views/Shared/_BoostrapScripts.cshtml")
                .ConfigureAwait(false));
            return contentBuilder;
        }

        public static Task<IHtmlContent> BootstrapCss(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_BootstrapCss.cshtml");

        public static Task<IHtmlContent> BootstrapGridCss(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_BootstrapCssGrid.cshtml");

        public static Task<IHtmlContent> BootstrapRebootCss(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_BootstrapCssReboot.cshtml");
    }
}

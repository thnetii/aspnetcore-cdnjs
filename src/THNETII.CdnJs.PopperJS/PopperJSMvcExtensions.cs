using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace THNETII.CdnJs
{
    public static class PopperJSMvcExtensions
    {
        public static IMvcBuilder AddJQueryApplicationPart(this IMvcBuilder mvc)
            => (mvc ?? throw new ArgumentNullException(nameof(mvc)))
                .AddApplicationPart(typeof(PopperJSMvcExtensions).Assembly);

        public static Task<IHtmlContent> PopperJsUmdScripts(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_PopperJsUmdScripts.cshtml");

        public static Task<IHtmlContent> PopperJsLiteUmdScripts(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_PopperJsLiteUmdScripts.cshtml");
    }
}

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace THNETII.CdnJs
{
    public static class JQueryMvcExtensions
    {
        public static IMvcBuilder AddPopperJSApplicationPart(this IMvcBuilder mvc)
            => (mvc ?? throw new ArgumentNullException(nameof(mvc)))
                .AddApplicationPart(typeof(JQueryMvcExtensions).Assembly);

        public static Task<IHtmlContent> JQueryScripts(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_JQueryScripts.cshtml");

        public static Task<IHtmlContent> JQuerySlimScripts(this IHtmlHelper html) =>
            (html ?? throw new ArgumentNullException(nameof(html)))
                .PartialAsync("/Views/Shared/_JQuerySlimScripts.cshtml");
    }
}

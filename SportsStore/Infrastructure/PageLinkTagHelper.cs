using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url")]
    public Dictionary<string, object> PageUrlValues { get; set; } = new();

    public PagingInfo? PageModel { get; set; }
    public string? PageAction { get; set; }
    public bool PageClassEnabled { get; set; } = false;
    public string PageClass { get; set; } = string.Empty;
    public string PageClassesNormal { get; set; } = string.Empty;
    public string PageClassSelected { get; set; } = string.Empty;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) => _urlHelperFactory = urlHelperFactory;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is not null && PageModel is not null)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tagBuilder = new("a");
                PageUrlValues["productPage"] = i;
                tagBuilder.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassEnabled)
                {
                    tagBuilder.AddCssClass(PageClass);
                    tagBuilder.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected
                                                                      : PageClassesNormal);
                }
                _ = tagBuilder.InnerHtml.Append(i.ToString());
                _ = result.InnerHtml.AppendHtml(tagBuilder);
            }

            _ = output.Content.AppendHtml(result.InnerHtml);
        }
    }
}

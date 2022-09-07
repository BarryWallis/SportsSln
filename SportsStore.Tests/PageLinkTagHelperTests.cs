using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Moq;

using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests;
public class PageLinkTagHelperTests
{
    [Fact]
    public void CanGeneratePageLinks()
    {
        Mock<IUrlHelper> urlHelper = new();
        _ = urlHelper.SetupSequence(uh => uh.Action(It.IsAny<UrlActionContext>()))
                     .Returns("Test/Page1")
                     .Returns("Test/Page2")
                     .Returns("Test/Page3");
        Mock<IUrlHelperFactory> urlHelperFactory = new();
        _ = urlHelperFactory.Setup(uhf => uhf.GetUrlHelper(It.IsAny<ActionContext>()))
                            .Returns(urlHelper.Object);
        Mock<ViewContext> viewContext = new();
        PageLinkTagHelper pageLinkTagHelper = new(urlHelperFactory.Object)
        {
            PageModel = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10,
            },
            ViewContext = viewContext.Object,
            PageAction = "Test"
        };
        TagHelperContext tagHelperContext = new(new TagHelperAttributeList(),
                                                new Dictionary<object, object>(),
                                                "");
        Mock<TagHelperContent> tagHelperContent = new();
        TagHelperOutput tagHelperOutput = new("div",
                                              new TagHelperAttributeList(),
                                              (cache, e)
                                                => Task.FromResult(tagHelperContent.Object));

        pageLinkTagHelper.Process(tagHelperContext, tagHelperOutput);

        Assert.Equal(@"<a href=""Test/Page1"">1</a>"
                     + @"<a href=""Test/Page2"">2</a>"
                     + @"<a href=""Test/Page3"">3</a>", tagHelperOutput.Content.GetContent());
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission7.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission7.Models.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-okay")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        public PageInfo PageOkay { get; set; }
        public string PageAction { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageOkay.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                tb.Attributes["href"] = uh.Action(PageAction, new { PageNum = i });
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}

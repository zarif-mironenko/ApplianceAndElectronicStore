using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Infrastructure
{
    [HtmlTargetElement(tag: "display-name", Attributes = "for",
                       TagStructure = TagStructure.WithoutEndTag)]
    public class DisplayModelPropertyTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            output.Content.SetContent(For.Metadata.DisplayName);
        }
    }
}

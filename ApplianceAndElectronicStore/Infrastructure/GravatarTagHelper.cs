using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Infrastructure
{
    [HtmlTargetElement(tag: "gravatar", Attributes = "user-email",
                       TagStructure = TagStructure.WithoutEndTag)]
    public class GravatarTagHelper : TagHelper
    {
        public string UserEmail { get; set; }
        
        public int ImageSize { get; set; } = 22;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            using (MD5 md5 = MD5.Create()) {
                byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(UserEmail.ToLower()));
                string hash = BitConverter.ToString(result).Replace("-", "").ToLower();

                var queryString = QueryString.Create(new Dictionary<string, string> {
                    ["s"] = ImageSize.ToString(),
                    ["d"] = "mm",
                    ["r"] = "g"
                });

                string url = $"https://gravatar.com/avatar/{hash}{queryString}";

                output.TagName = "img";
                output.Attributes.SetAttribute("src", url);
            } // using
        }
    }
}

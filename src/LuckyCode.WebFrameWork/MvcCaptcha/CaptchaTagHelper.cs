using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LuckyCode.WebFrameWork.MvcCaptcha
{
    [HtmlTargetElement("captcha")]
    public class CaptchaTagHelper:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private static string key = Guid.NewGuid().ToString("N");
        public string Src { get; set; } = $"/____Capcha?key={key}";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "image";
            output.Attributes.Add("src",Src);
            output.Attributes.Add("onclick", "this.src=this.src.split('?')[0]+'?'+Math.random()+'&'+'key="+key+"'");
            output.PreContent.SetHtmlContent($"<input type='hidden' name='key' value='{key}'/>");
        }
    }
}

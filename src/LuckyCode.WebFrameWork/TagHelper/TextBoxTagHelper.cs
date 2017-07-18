using LuckyCode.Core.Utility.Extensions;
using LuckyCode.WebFrameWork.Rendering;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LuckyCode.WebFrameWork.TagHelper {

    [HtmlTargetElement(TagName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TextBoxTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper {

        private const string TagName = "ss:textbox";

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext {
            get; set;
        }
        [HtmlAttributeName("for")]
        public ModelExpression For {
            get; set;
        }
        [HtmlAttributeName("name")]
        public string Name {
            get; set;
        }
        [HtmlAttributeName("value")]
        public string Value {
            get; set;
        }
        [HtmlAttributeName("placeholder")]
        public string PlaceHolder {
            get; set;
        }
        [HtmlAttributeName("addon")]
        public string Addon {
            get; set;
        }
        [HtmlAttributeName("addon-position")]
        public AddonPosition AddonPosition {
            get; set;
        } = AddonPosition.Left;

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var builder = new FormTextBoxBuilder(ViewContext);

            //配置属性
            builder.Configure(options => {
                //复制属性,注意命名要一致
                this.CloneTo(options);

                if (For != null) {
                    if (string.IsNullOrEmpty(options.Name)) {
                        options.Name = For.Name;
                    }
                    if (options.Value == null && For.Model != null) {
                        options.Value = For.Model;
                    }
                }
            });
            //合并属性
            builder.MergeAttributes(output);

            output.TagName = null;

            output.Content.SetHtmlContent(builder);
        }

    }
}

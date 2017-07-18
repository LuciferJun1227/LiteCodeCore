using LuckyCode.Core.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckyCode.WebFrameWork.Rendering {
    /// <summary>
    /// 文本框构建器，基于Bootstrap 样式封装
    /// </summary>
    public class FormTextBoxBuilder : HtmlBuilder<FormTextBoxBuilder, FormTextBoxBuilderOptions> {

        public FormTextBoxBuilder(ViewContext viewContext) : base(viewContext) {
        }

        protected override void Render(HtmlWriter writer) {
            //文本框扩展信息
            if (!string.IsNullOrEmpty(Options.Addon)) {
                writer.WriteClass("input-group");
                writer.WriteTag(HtmlWriterTag.Div);
                if (Options.AddonPosition == AddonPosition.Left) {
                    RenderAddon(writer);
                }
            }
            writer.WriteName(Options.Name);
            writer.WriteValue(Options.Value);
            writer.WritePlaceHolder(Options.PlaceHolder);
            writer.WriteClass("form-control");
            //合并自定义属性
            writer.MergeAttributes(Attribues);
            writer.WriteCloseTag(HtmlWriterTag.Input);
            if (!string.IsNullOrEmpty(Options.Addon)) {
                if (Options.AddonPosition == AddonPosition.Right) {
                    RenderAddon(writer);
                }
                writer.WriteEndTag();
            }
        }

        private void RenderAddon(HtmlWriter writer) {
            writer.WriteClass("input-group-addon");
            writer.WriteTag(HtmlWriterTag.Span, true);
            writer.Write(Options.Addon);
            writer.WriteEndTag();
        }
    }
    /// <summary>
    /// 表示输入框属性
    /// </summary>
    public class FormTextBoxBuilderOptions : FormElementBuilderOptions {
        /// <summary>
        /// 占位文字
        /// </summary>
        public string PlaceHolder {
            get; set;
        }
        /// <summary>
        /// 扩展信息内容
        /// </summary>
        public string Addon {
            get; set;
        }
        /// <summary>
        /// 扩展信息位置
        /// </summary>
        public AddonPosition AddonPosition {
            get; set;
        } = AddonPosition.Left;

    }
    /// <summary>
    /// 扩展信息位置枚举
    /// </summary>
    public enum AddonPosition {
        /// <summary>
        /// 左边
        /// </summary>
        Left,
        /// <summary>
        /// 右边
        /// </summary>
        Right
    }
}
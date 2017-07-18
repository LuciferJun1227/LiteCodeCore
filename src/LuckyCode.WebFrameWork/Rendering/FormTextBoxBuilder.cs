using LuckyCode.Core.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckyCode.WebFrameWork.Rendering {
    /// <summary>
    /// �ı��򹹽���������Bootstrap ��ʽ��װ
    /// </summary>
    public class FormTextBoxBuilder : HtmlBuilder<FormTextBoxBuilder, FormTextBoxBuilderOptions> {

        public FormTextBoxBuilder(ViewContext viewContext) : base(viewContext) {
        }

        protected override void Render(HtmlWriter writer) {
            //�ı�����չ��Ϣ
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
            //�ϲ��Զ�������
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
    /// ��ʾ���������
    /// </summary>
    public class FormTextBoxBuilderOptions : FormElementBuilderOptions {
        /// <summary>
        /// ռλ����
        /// </summary>
        public string PlaceHolder {
            get; set;
        }
        /// <summary>
        /// ��չ��Ϣ����
        /// </summary>
        public string Addon {
            get; set;
        }
        /// <summary>
        /// ��չ��Ϣλ��
        /// </summary>
        public AddonPosition AddonPosition {
            get; set;
        } = AddonPosition.Left;

    }
    /// <summary>
    /// ��չ��Ϣλ��ö��
    /// </summary>
    public enum AddonPosition {
        /// <summary>
        /// ���
        /// </summary>
        Left,
        /// <summary>
        /// �ұ�
        /// </summary>
        Right
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using LuckyCode.Core.Utility;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LuckyCode.WebFrameWork.Rendering {

    /// <summary>
    /// Html构建器
    /// </summary>
    public abstract class HtmlBuilder : IHtmlContent {
        private bool _hasRenderScripts = false;
        protected HtmlBuilder(ViewContext viewContext) {
            ViewContext = viewContext;

        }
        protected ViewContext ViewContext {
            get;
        }

        protected readonly HtmlWriter Writer = new HtmlWriter();

        internal virtual void RenderScripts() {

        }
        /// <summary>
        /// 呈现
        /// </summary>
        /// <param name="writer"></param>
        protected abstract void Render(HtmlWriter writer);


        public virtual void WriteTo(TextWriter writer, HtmlEncoder encoder) {
            if (!_hasRenderScripts) {
                RenderScripts();
                _hasRenderScripts = true;
            }
            Render(Writer);
            var html = Writer.ToString();
            writer.Write(html);
        }
        protected IDictionary<string, object> Attribues {
            get; set;
        } = new Dictionary<string, object>();

        public void MergeAttributes(TagHelperOutput output) {
            foreach (var attribute in output.Attributes) {
                Attribues[attribute.Name] = attribute.Value;
            }
        }
    }

    public abstract class HtmlBuilder<TBuilder, TOptions> : HtmlBuilder
        where TBuilder : HtmlBuilder<TBuilder, TOptions>
        where TOptions : new() {
        private readonly TOptions _option = new TOptions();

        protected HtmlBuilder(ViewContext viewContext) : base(viewContext) {

        }

        public TBuilder ConfigureAttributes(Action<IDictionary<string, object>> configure) {
            configure?.Invoke(Attribues);
            return (TBuilder)this;
        }
        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public virtual TBuilder Configure(Action<TOptions> configure) {
            configure?.Invoke(_option);
            return (TBuilder)this;
        }
        /// <summary>
        /// 默认选项
        /// </summary>
        protected TOptions Options => _option;


    }


}
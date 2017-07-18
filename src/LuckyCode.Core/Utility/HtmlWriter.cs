using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using LuckyCode.Core.Utility.Extensions;
using Microsoft.AspNetCore.Html;

namespace LuckyCode.Core.Utility {
    /// <summary>
    /// Html构建
    /// </summary>
    public class HtmlWriter : HtmlAttributeWriter, IHtmlContent {

        private readonly StringBuilder _builder = new StringBuilder();
        private readonly Stack<string> _tags = new Stack<string>();
        private int lastIndent = 0;

        public void Write(IHtmlContent content) {
            if (content != null) {
                Write(content.ToHtmlString());
            }
        }

        public void Write(object value) {
            if (value != null && value.ToString() != "") {
                _builder.Append(value);
            }
        }

        public void WriteLine(object obj = null) {
            _builder.AppendLine();
            if (obj != null) {
                Write(obj);
            }
        }

        public void WriteIndent(int count = 1) {
            _builder.Append(String.Empty.PadLeft(count, '\t'));
        }

        public void WriteTag(HtmlWriterTag tag, bool inline = false) {
            if (tag != HtmlWriterTag.Unknown) {
                WriteTag(tag.ToString().ToLower(), inline);
            }
        }

        public void WriteTag(string tagName, bool inline = false) {
            if (_tags.Count > 1) {
                WriteIndent(_tags.Count - 1);
                lastIndent = _tags.Count - 1;
            }
            Write("<");
            Write(tagName);
            Write(FlushStringAttribtes());
            Write(">");
            if (!inline) {
                WriteLine();
            }
            _tags.Push(tagName);
        }

        public void WriteCloseTag(HtmlWriterTag tag) {
            WriteCloseTag(tag.ToString().ToLower());
        } 
        public void WriteCloseTag(string tagName) {
            if (lastIndent > 0) {
                WriteIndent(lastIndent);
            }
            Write("<");
            Write(tagName);
            Write(FlushStringAttribtes());
            Write(" />");
            WriteLine();
        }

        public void WriteEndTag() {
            if (_tags.Count == 0) {
                return;
            }
            if (_tags.Count >= 2) {
                WriteIndent(_tags.Count - 2);
                lastIndent = _tags.Count - 2;
            } else {
                WriteIndent(lastIndent);
            }
            var current = _tags.Pop();
            if (string.IsNullOrEmpty(current)) {
                throw new Exception("没有匹配的开始标签");
            }
            Write($"</{current}>");
            WriteLine();

        }
        public override string ToString() {
            if (_tags.Count > 0) {
                while (_tags.Count > 0) {
                    WriteEndTag();
                }
            }
            return _builder.ToString();
        }
        public HtmlString ToHtmlString() {
            return new HtmlString(ToString());
        }
        public void WriteTo(TextWriter writer, HtmlEncoder encoder) {
            writer.Write(ToString());
        }

    }
    public enum HtmlWriterTag {
        Unknown = 0,
        A = 1,
        Acronym = 2,
        Address = 3,
        Area = 4,
        B = 5,
        Base = 6,
        Basefont = 7,
        Bdo = 8,
        Bgsound = 9,
        Big = 10,
        Blockquote = 11,
        Body = 12,
        Br = 13,
        Button = 14,
        Caption = 15,
        Center = 16,
        Cite = 17,
        Code = 18,
        Col = 19,
        Colgroup = 20,
        Dd = 21,
        Del = 22,
        Dfn = 23,
        Dir = 24,
        Div = 25,
        Dl = 26,
        Dt = 27,
        Em = 28,
        Embed = 29,
        Fieldset = 30,
        Font = 31,
        Form = 32,
        Frame = 33,
        Frameset = 34,
        H1 = 35,
        H2 = 36,
        H3 = 37,
        H4 = 38,
        H5 = 39,
        H6 = 40,
        Head = 41,
        Hr = 42,
        Html = 43,
        I = 44,
        Iframe = 45,
        Img = 46,
        Input = 47,
        Ins = 48,
        Isindex = 49,
        Kbd = 50,
        Label = 51,
        Legend = 52,
        Li = 53,
        Link = 54,
        Map = 55,
        Marquee = 56,
        Menu = 57,
        Meta = 58,
        Nobr = 59,
        Noframes = 60,
        Noscript = 61,
        Object = 62,
        Ol = 63,
        Option = 64,
        P = 65,
        Param = 66,
        Pre = 67,
        Q = 68,
        Rt = 69,
        Ruby = 70,
        S = 71,
        Samp = 72,
        Script = 73,
        Select = 74,
        Small = 75,
        Span = 76,
        Strike = 77,
        Strong = 78,
        Style = 79,
        Sub = 80,
        Sup = 81,
        Table = 82,
        Tbody = 83,
        Td = 84,
        Textarea = 85,
        Tfoot = 86,
        Th = 87,
        Thead = 88,
        Title = 89,
        Tr = 90,
        Tt = 91,
        U = 92,
        Ul = 93,
        Var = 94,
        Wbr = 95,
        Xml = 96
    }
}

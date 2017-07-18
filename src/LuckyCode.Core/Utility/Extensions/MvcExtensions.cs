using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LuckyCode.Core.Utility.Extensions {
    public static class MvcExtensions {
        public static string ToHtmlString(this IHtmlContent content) {
            StringBuilder sb = new StringBuilder();
            StringWriter stringWriter = new StringWriter(sb);
            content.WriteTo(stringWriter, HtmlEncoder.Default);
            return sb.ToString();
        }

        public static string ToTrimHtmlString(this IHtmlContent content) {
            return content.ToHtmlString().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim();
        }

        public static string[] Errors(this ModelStateDictionary modelState, bool allowKeyName = false) {
            var errors = new List<string>();
            if (!modelState.IsValid) {
                foreach (var s in modelState) {
                    var message = string.Join(",", s.Value.Errors.Select(t => t.ErrorMessage));
                    if (string.IsNullOrEmpty(message)) {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(s.Key) && allowKeyName) {
                        errors.Add(s.Key + ":" + message);
                    } else {
                        errors.Add(message);
                    }
                }
                return errors.ToArray();

            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LuckyCode.Core.Utility;
using LuckyCode.Core.Utility.Extensions;
using LuckyCode.WebFrameWork.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace LuckyCode.WebFrameWork.HtmlExtensions {
    public static class HtmlHelperExtensions {
        public static IHtmlContent DropdownlistTrueOrFalse<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null) {
            List<SelectListItem> slist = new List<SelectListItem>();
            slist.Add(new SelectListItem() { Value = "false", Text = "否", Selected = true });
            slist.Add(new SelectListItem() { Value = "true", Text = "是" });
            string name = ExpressionHelper.GetExpressionText(expression);
            return helper.DropDownList(name, slist, htmlAttributes);
        }
        public static IHtmlContent DropDownListTreeFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, List<ListItemEntity> list, string pid, bool flag = true, object htmlAttributes = null) {
            string name = ExpressionHelper.GetExpressionText(expression);
            return helper.DropDownListTree(name, list, pid, flag, htmlAttributes);
        }
        public static IHtmlContent DropDownListTree(this IHtmlHelper helper, string name, List<ListItemEntity> list, string pid, bool flag = true, object htmlAttributes = null) {
            List<SelectListItem> slist = new List<SelectListItem>();
            if (flag) {
                slist.Add(new SelectListItem() { Text = "-----请选择----", Value = "0", Selected = true });
            }
            foreach (ListItemEntity entity in list.Where(a => a.ParentID == pid)) {
                slist.Add(new SelectListItem() { Text = entity.Title, Value = entity.ID, Selected = entity.Selected });
                BuildTree(list, slist, entity.ID, 1);
            }

            return helper.DropDownList(name, slist, htmlAttributes);
        }
        private static void BuildTree(List<ListItemEntity> list, List<SelectListItem> slist, string pid, int i) {
            var _list = list.Where(a => a.ParentID == pid);
            foreach (ListItemEntity entity in _list) {
                string pre = " ";
                for (int j = 0; j < i; j++) {
                    if (j == 0) {
                        pre = pre + "┣";
                    } else {
                        pre = "　" + pre;
                    }
                }
                slist.Add(new SelectListItem { Text = pre + entity.Title, Value = entity.ID, Selected = entity.Selected });
                BuildTree(list, slist, entity.ID, i + 1);
            }
        }

        public static FormTextBoxBuilder SSTextBox<TModel, TProperty>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string value = null,
            object attrs = null) {
            string name = ExpressionHelper.GetExpressionText(expression);
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, helper.ViewData, helper.MetadataProvider);
            return new FormTextBoxBuilder(helper.ViewContext).Configure(options => {
                options.Name = name;
                options.Value = value ?? modelExplorer.Model;
                if (attrs != null) {
                    options.Attributes = (Dictionary<string, object>)attrs.ToDictionary();
                }
            });
        }
    }
}

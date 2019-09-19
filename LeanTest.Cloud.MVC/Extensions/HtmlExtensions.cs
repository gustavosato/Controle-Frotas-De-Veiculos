using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class HtmlExtensions
    {
        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return id.Replace('[', '_').Replace(']', '_');
        }
    }
}
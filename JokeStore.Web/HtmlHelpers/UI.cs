using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JokeStore.Web.Models;
using JokeStore.Web.Core;

namespace JokeStore.Web.HtmlHelpers
{
    public static class UI
    {
        public static MvcHtmlString PageLinks(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pagingInfo.TotalPages > 1)
            {
                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                        tag.AddCssClass("selected");
                    result.Append(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString CalculatePecentage(int total, int count)
        {
            if(total == 0)
                return MvcHtmlString.Create(0M.ToString());

            double value = (double)count / total;
            return MvcHtmlString.Create(Math.Round((value * 100)).ToString());
        }

        public static MvcHtmlString Message(HtmlMessage message)
        {
            if (message != null)
            {
                TagBuilder tag = new TagBuilder("div");
                tag.AddCssClass("message");
                tag.AddCssClass(message.CssClass);
                tag.InnerHtml = "<div class=\"message-icon\"></div><div class=\"message-content\">" + message.Content + "</div><div class=\"clear\"></div>";
                return MvcHtmlString.Create(tag.ToString());
            }
            else
            {
                return MvcHtmlString.Create("");
            }
        }

        public static MvcHtmlString CutString(string content, int length = 20)
        {
            return MvcHtmlString.Create(StringUtil.Cut(content, length));
        }
    }
}
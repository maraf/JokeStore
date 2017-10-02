using JokeStore.Web.Models;
using JokeStore.Web.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JokeStore.Web.HtmlHelpers
{
    public static class UI
    {
        public static string PageLinks(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pagingInfo.TotalPages > 1)
            {
                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml.AppendHtml(i.ToString());
                    if (i == pagingInfo.CurrentPage)
                        tag.AddCssClass("selected");
                    result.Append(tag.ToString());
                }
            }
            return result.ToString();
        }

        public static string CalculatePecentage(int total, int count)
        {
            if(total == 0)
                return 0M.ToString();

            double value = (double)count / total;
            return Math.Round((value * 100)).ToString();
        }

        public static string Message(HtmlMessage message)
        {
            if (message != null)
            {
                TagBuilder tag = new TagBuilder("div");
                tag.AddCssClass("message");
                tag.AddCssClass(message.CssClass);
                tag.InnerHtml.AppendHtml("<div class=\"message-icon\"></div><div class=\"message-content\">" + message.Content + "</div><div class=\"clear\"></div>");
                return tag.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string CutString(string content, int length = 20)
        {
            return StringUtil.Cut(content, length);
        }
    }
}
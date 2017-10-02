using JokeStore.Web.Models;
using JokeStore.Web.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Html;

namespace JokeStore.Web.HtmlHelpers
{
    public static class UI
    {
        public static HtmlString PageLinks(PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pagingInfo.TotalPages > 1)
            {
                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    string cssClass = pagingInfo.CurrentPage == i ? " class='selected'" : "";
                    result.Append($"<a href='{pageUrl(i)}'{cssClass}>{i}</a>");
                }
            }

            return new HtmlString(result.ToString());
        }

        public static string CalculatePecentage(int total, int count)
        {
            if (total == 0)
                return 0M.ToString();

            double value = (double)count / total;
            return Math.Round((value * 100)).ToString();
        }

        public static HtmlString Message(HtmlMessage message)
        {
            if (message != null)
                return new HtmlString($"div class='message {message.CssClass}'><div class=\"message-icon\"></div><div class=\"message-content\">{message.Content}</div><div class=\"clear\"></div></div>");
            else
                return HtmlString.Empty;
        }

        public static string CutString(string content, int length = 20)
        {
            return StringUtil.Cut(content, length);
        }
    }
}
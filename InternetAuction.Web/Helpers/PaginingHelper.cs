using InternetAuction.Web.Infrastructure;
using System;
using System.Text;
using System.Web.Mvc;

namespace InternetAuction.Web.Helpers
{
    /// <summary>
    /// Represents pagination helper class
    /// </summary>
    public static class PaginingHelper
    {
        /// <summary>
        /// Creates page links
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-dark");
                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }

}
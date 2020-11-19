﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace InternetAuction.Web.Helpers
{
    public static class AlertHelper
    {
        public static MvcHtmlString ShowAlert(this HtmlHelper html, string text, string alertColor)
        {
            var div = new TagBuilder("div");
            var divAttributes = new Dictionary<string, string>
            {
                { "class", $"alert alert-{alertColor} alert-dismissible fade show" },
                { "role", "alert" }
            };
            div.MergeAttributes(divAttributes);

            var strong = new TagBuilder("strong");
            strong.InnerHtml = text;

            var button = new TagBuilder("button");
            var buttonAttributes = new Dictionary<string, string>
            {
                { "class", "close" },
                { "type", "button" },
                { "data-dismiss", "alert" },
                { "aria-label", "Close" },
            };
            button.MergeAttributes(buttonAttributes);

            var span = new TagBuilder("span");
            span.MergeAttribute("aria-hidden", "true");
            span.InnerHtml = "&times;";

            button.InnerHtml += span;

            div.InnerHtml += strong;
            div.InnerHtml += button;

            return new MvcHtmlString(div.ToString());
        }
    }
}
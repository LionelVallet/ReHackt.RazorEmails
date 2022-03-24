// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ReHackt.RazorEmails.TagHelpers
{
    [HtmlTargetElement("email-img", Attributes = nameof(Src), TagStructure = TagStructure.WithoutEndTag)]
    public class EmailImageTagHelper : TagHelper
    {
        public string? Alt { get; set; }

        public string? Href { get; set; }

        public string? Src { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrWhiteSpace(Src))
            {
                output.TagName = "table";
                output.Attributes.SetAttribute("width", "100%");
                output.Attributes.SetAttribute("border", "0");
                output.Attributes.SetAttribute("cellspacing", "0");
                output.Attributes.SetAttribute("cellpadding", "0");
                var htmlContent = @"<tr><td bgcolor=""#ffffff"" align=""left"" style=""padding: 10px 0px 30px 0px;"">";
                if (!string.IsNullOrWhiteSpace(Href))
                {
                    htmlContent += $@"<a href=""{Href}"" target=""_blank"">";
                }
                htmlContent += $@"<img alt=""{Alt}"" src=""{Src}"" width=""600"" style=""display: block; width: 100%; max-width: 100%; min-width: 100px; font-family: 'Lato', Helvetica, Arial, sans-serif; color: #ffffff; font-size: 18px;"" border=""0"">";
                if (!string.IsNullOrWhiteSpace(Href))
                {
                    htmlContent += "</a>";
                }
                htmlContent += "</td></tr>";
                output.Content.SetHtmlContent(htmlContent);
                output.TagMode = TagMode.StartTagAndEndTag;
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}

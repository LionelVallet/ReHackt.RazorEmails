// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace ReHackt.Emails.TagHelpers
{
    [HtmlTargetElement("email-h2", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class EmailHeadlineTagHelper : TagHelper
    {
        public EmailHeadlineTagHelper(IOptionsMonitor<EmailOptions> emailOptions)
        {
            Color = emailOptions.CurrentValue.Template?.HeadlineColor;
        }

        public string? Color { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = (await output.GetChildContentAsync()).GetContent();
            if (!string.IsNullOrWhiteSpace(content))
            {
                output.TagName = "table";
                output.Attributes.SetAttribute("width", "100%");
                output.Attributes.SetAttribute("border", "0");
                output.Attributes.SetAttribute("cellspacing", "0");
                output.Attributes.SetAttribute("cellpadding", "0");
                output.Content.SetHtmlContent(
                $@"<tr>
                    <td bgcolor=""#ffffff"" align=""left"" style=""padding: 20px 30px 0px 30px; color: {Color}; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"" >
                        <h2 style=""font-size: 24px; font-weight: 400; margin: 0;"">{content}</p>
                    </td>
                </tr>");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}

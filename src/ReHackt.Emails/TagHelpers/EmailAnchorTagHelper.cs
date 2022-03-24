// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace ReHackt.Emails.TagHelpers
{
    [HtmlTargetElement("email-a", Attributes = nameof(Href), TagStructure = TagStructure.NormalOrSelfClosing)]
    public class EmailAnchorTagHelper : TagHelper
    {
        public EmailAnchorTagHelper(IOptionsMonitor<EmailOptions> emailOptions)
        {
            Color = emailOptions.CurrentValue.Template?.LinkColor;
        }

        public string? Color { get; set; }

        public string? Href { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrWhiteSpace(Href))
            {
                output.TagName = "table";
                output.Attributes.SetAttribute("width", "100%");
                output.Attributes.SetAttribute("border", "0");
                output.Attributes.SetAttribute("cellspacing", "0");
                output.Attributes.SetAttribute("cellpadding", "0");
                output.Content.SetHtmlContent(
                $@"<tr>
                <td bgcolor=""#ffffff"" align=""left"" style=""padding: 0px 30px 20px 30px; color: {Color}; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"" >
                    <p style=""margin: 0;""><a href=""{Href}"" target=""_blank"" style=""color: {Color}; word-break: break-all;"">{(await output.GetChildContentAsync()).GetContent()}</a></p>
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

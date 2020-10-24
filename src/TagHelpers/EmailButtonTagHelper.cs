using ReHackt.RazorEmails.Configuration;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ReHackt.RazorEmails.TagHelpers
{
    [HtmlTargetElement("email-button", Attributes = nameof(Href), TagStructure = TagStructure.NormalOrSelfClosing)]
    public class EmailButtonTagHelper : TagHelper
    {
        public EmailButtonTagHelper(IOptionsMonitor<EmailOptions> emailOptions)
        {
            BackgroundColor = emailOptions.CurrentValue.Template.ButtonBackgroundColor;
            Color = emailOptions.CurrentValue.Template.ButtonTextColor;
        }

        public string BackgroundColor { get; set; }

        public string Color { get; set; }

        public string Href { get; set; }

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
                    <td bgcolor=""#ffffff"" align=""center"" style=""padding: 20px 30px 40px 30px;"">
                        <table border=""0"" cellspacing=""0"" cellpadding=""0"">
                            <tr>
                                <td align=""center"" style=""border-radius: 3px;"" bgcolor=""{BackgroundColor}"">
                                    <a href=""{Href}"" target=""_blank"" style=""font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: {Color}; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid {BackgroundColor}; display: inline-block;"">
                                        {(await output.GetChildContentAsync()).GetContent()}
                                    </a>
                                </td>
                            </tr>
                        </table>
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

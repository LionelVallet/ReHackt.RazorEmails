using ReHackt.RazorEmails.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace ReHackt.RazorEmails.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailService> _logger;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public EmailService(
            IOptionsMonitor<EmailOptions> emailOptions,
            ILogger<EmailService> logger,
            IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailOptions = emailOptions.CurrentValue;
            _logger = logger;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task SendEmailAsync(string subject, string body, string recipientEmail, string recipientName = null)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_emailOptions.Smtp.Host, _emailOptions.Smtp.Port, _emailOptions.Smtp.EnableSsl);
                if (!string.IsNullOrEmpty(_emailOptions.Smtp.Username))
                {
                    await client.AuthenticateAsync(_emailOptions.Smtp.Username, _emailOptions.Smtp.Password);
                }
                await client.SendAsync(new MimeMessage(
                    new[] { new MailboxAddress(_emailOptions.SenderName ?? _emailOptions.SenderEmail, _emailOptions.SenderEmail) },
                    new[] { new MailboxAddress(recipientName, recipientEmail) },
                    subject,
                    new TextPart(TextFormat.Html) { Text = body }
                ));
                _logger.LogInformation("Email sent.");
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't send email");
                throw;
            }
        }

        public async Task SendRazorViewEmailAsync<TModel>(string subject, string viewName, TModel model, string recipientEmail, string recipientName = null)
        {
            await SendEmailAsync(subject, await _razorViewToStringRenderer.RenderViewToStringAsync(viewName, model), recipientEmail, recipientName);
        }
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, string recipientEmail, string recipientName = null);

        Task SendRazorViewEmailAsync<TModel>(string subject, string viewName, TModel model, string recipientEmail, string recipientName = null);
    }
}

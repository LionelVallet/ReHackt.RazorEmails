// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ReHackt.RazorEmails.Configuration;

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

        public async Task SendEmailAsync(string subject, string body, string recipientEmail, string? recipientName = null)
        {
            try
            {
                var smtpOptions = _emailOptions.Smtp!;
                using var client = new SmtpClient();
                await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, smtpOptions.EnableSsl);
                if (!string.IsNullOrEmpty(smtpOptions.Username))
                {
                    await client.AuthenticateAsync(smtpOptions.Username, smtpOptions.Password);
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

        public async Task SendRazorViewEmailAsync<TModel>(string subject, string viewName, TModel model, string recipientEmail, string? recipientName = null)
        {
            await SendEmailAsync(subject, await _razorViewToStringRenderer.RenderViewToStringAsync(viewName, model), recipientEmail, recipientName);
        }
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, string recipientEmail, string? recipientName = null);

        Task SendRazorViewEmailAsync<TModel>(string subject, string viewName, TModel model, string recipientEmail, string? recipientName = null);
    }
}

// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace ReHackt.Emails.Services
{
    public class SmtpEmailService : EmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly SmtpOptions _smtpOptions;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(
            IOptionsMonitor<EmailOptions> emailOptions,
            IOptionsMonitor<SmtpOptions> smtpOptions,
            ILogger<SmtpEmailService> logger,
            IRazorViewToStringRenderer razorViewToStringRenderer)
            : base(razorViewToStringRenderer)
        {
            _emailOptions = emailOptions.CurrentValue;
            _smtpOptions = smtpOptions.CurrentValue;
            _logger = logger;
        }

        public override async Task SendEmailAsync(string subject, string body, string recipientEmail, string? recipientName = null)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.EnableSsl);
                if (!string.IsNullOrEmpty(_smtpOptions.Username))
                {
                    await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
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
    }
}

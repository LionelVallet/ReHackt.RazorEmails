// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReHackt.Emails;
using ReHackt.Emails.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailBuilderExtensions
    {
        public static IEmailBuilder AddSmtpEmailService(this IEmailBuilder builder)
        {
            builder.Services.AddScoped<IEmailService, SmtpEmailService>();

            return builder;
        }

        public static IEmailBuilder AddSmtpEmailService(this IEmailBuilder builder, IConfiguration? configuration = null)
        {
            return builder.AddSmtpEmailService(options => configuration.Bind(options));
        }

        public static IEmailBuilder AddSmtpEmailService(this IEmailBuilder builder, Action<SmtpOptions> configureOptions)
        {
            builder.Services
                .AddOptions<SmtpOptions>()
                .Configure(configureOptions)
                .ValidateDataAnnotationsRecursively()
                .Validate<ILogger<EmailService>>(ValidateSmtpOptions, "Failed to connect to SMTP server")
                .ValidateOnStart();

            return builder.AddSmtpEmailService();
        }

        private static bool ValidateSmtpOptions(SmtpOptions options, ILogger<EmailService> logger)
        {
            try
            {
                using var client = new SmtpClient();
                client.Connect(options.Host, options.Port, options.EnableSsl);
                if (!string.IsNullOrEmpty(options.Username))
                {
                    client.Authenticate(options.Username, options.Password);
                }
                client.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}

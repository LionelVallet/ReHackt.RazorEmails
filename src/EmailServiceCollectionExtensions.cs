// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReHackt.RazorEmails.Configuration;
using ReHackt.RazorEmails.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorEmails(this IServiceCollection services)
        {
            return services == null
                ? throw new ArgumentNullException(nameof(services))
                : services
                .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
                .AddScoped<IEmailService, EmailService>();
        }

        public static IServiceCollection AddRazorEmails(this IServiceCollection services, IConfiguration? configuration = null)
        {
            return services == null
                ? throw new ArgumentNullException(nameof(services))
                : services
                .AddRazorEmails()
                .AddOptions<EmailOptions>()
                    .Bind(configuration)
                    .ValidateEmailOptions()
                    .Services;
        }

        public static IServiceCollection AddRazorEmails(this IServiceCollection services, Action<EmailOptions> emailOptions)
        {
            return services == null
                ? throw new ArgumentNullException(nameof(services))
                : services
                .AddRazorEmails()
                .AddOptions<EmailOptions>()
                    .Configure(emailOptions)
                    .ValidateEmailOptions()
                    .Services;
        }

        private static OptionsBuilder<EmailOptions> ValidateEmailOptions(this OptionsBuilder<EmailOptions> optionsBuilder)
        {
            return optionsBuilder == null
                ? throw new ArgumentNullException(nameof(optionsBuilder))
                : optionsBuilder
                    .ValidateDataAnnotationsRecursively()
                    .Validate<ILogger<EmailService>>(ValidateSmtpOptions, "Failed to connect to SMTP server")
                    .ValidateOnStart();
        }

        private static bool ValidateSmtpOptions(EmailOptions options, ILogger<EmailService> logger)
        {
            try
            {
                var smtpOptions = options.Smtp!;
                using var client = new SmtpClient();
                client.Connect(smtpOptions.Host, smtpOptions.Port, smtpOptions.EnableSsl);
                if (!string.IsNullOrEmpty(smtpOptions.Username))
                {
                    client.Authenticate(smtpOptions.Username, smtpOptions.Password);
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

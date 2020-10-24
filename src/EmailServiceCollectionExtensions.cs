using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReHackt.RazorEmails.Configuration;
using ReHackt.RazorEmails.Services;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorEmails(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            return services
                .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
                .AddScoped<IEmailService, EmailService>();
        }

        public static IServiceCollection AddRazorEmails(this IServiceCollection services, IConfiguration configuration = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            return services
                .AddRazorEmails()
                .AddOptions<EmailOptions>()
                    .Bind(configuration)
                    .ValidateEmailOptions()
                    .Services;
        }

        public static IServiceCollection AddRazorEmails(this IServiceCollection services, Action<EmailOptions> emailOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            return services
                .AddRazorEmails()
                .AddOptions<EmailOptions>()
                    .Configure(emailOptions)
                    .ValidateEmailOptions()
                    .Services;
        }

        private static OptionsBuilder<EmailOptions> ValidateEmailOptions(this OptionsBuilder<EmailOptions> optionsBuilder)
        {
            if (optionsBuilder == null) throw new ArgumentNullException(nameof(optionsBuilder));
            return optionsBuilder
                    .ValidateDataAnnotationsRecursively()
                    .Validate<ILogger<EmailService>>(ValidateSmtpOptions, "Failed to connect to SMTP server")
                    .ValidateEagerly();
        }

        private static bool ValidateSmtpOptions(EmailOptions options, ILogger<EmailService> logger)
        {
            try
            {
                using var client = new SmtpClient();
                client.Connect(options.Smtp.Host, options.Smtp.Port, options.Smtp.EnableSsl);
                if (!string.IsNullOrEmpty(options.Smtp.Username))
                {
                    client.Authenticate(options.Smtp.Username, options.Smtp.Password);
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

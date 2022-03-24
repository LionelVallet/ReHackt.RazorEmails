// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Extensions.Configuration;
using ReHackt.Emails;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailServiceCollectionExtensions
    {
        /// <summary>
        /// Adds emails.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IEmailBuilder AddEmails(this IServiceCollection services)
        {
            var builder = new EmailBuilder(services);

            builder.AddRazorViewToStringRenderer();

            return builder;
        }

        /// <summary>
        /// Adds emails.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IEmailBuilder AddEmails(this IServiceCollection services, IConfiguration? configuration = null)
        {
            return services.AddEmails(options => configuration.Bind(options));
        }

        /// <summary>
        /// Adds emails.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configureOptions">The options configuration action.</param>
        /// <returns></returns>
        public static IEmailBuilder AddEmails(this IServiceCollection services, Action<EmailOptions> configureOptions)
        {
            services.ConfigureAndValidate(configureOptions);
            return services.AddEmails();
        }
    }
}

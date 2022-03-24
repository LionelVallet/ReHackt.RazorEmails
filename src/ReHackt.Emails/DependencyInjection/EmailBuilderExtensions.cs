// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using ReHackt.Emails;
using ReHackt.Emails.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailBuilderExtensions
    {
        public static IEmailBuilder AddRazorViewToStringRenderer(this IEmailBuilder builder)
        {
            builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            return builder;
        }
    }
}

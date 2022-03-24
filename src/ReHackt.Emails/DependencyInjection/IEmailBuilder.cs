// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Extensions.DependencyInjection;

namespace ReHackt.Emails
{
    /// <summary>
    /// Email builder Interface
    /// </summary>
    public interface IEmailBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }
}

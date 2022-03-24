// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

namespace ReHackt.Emails.Services
{
    public abstract class EmailService : IEmailService
    {
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public EmailService(IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public abstract Task SendEmailAsync(string subject, string body, string recipientEmail, string? recipientName = null);

        public virtual async Task SendRazorViewEmailAsync<TModel>(string subject, string viewName, TModel model, string recipientEmail, string? recipientName = null)
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

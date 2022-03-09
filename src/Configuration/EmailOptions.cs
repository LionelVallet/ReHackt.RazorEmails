// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel.DataAnnotations;

namespace ReHackt.RazorEmails.Configuration
{
    public class EmailOptions
    {
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        [Required]
        public SmtpOptions Smtp { get; set; }

        [Required]
        public TemplateOptions Template { get; set; }
    }
}

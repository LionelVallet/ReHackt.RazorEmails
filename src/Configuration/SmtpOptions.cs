// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel.DataAnnotations;

namespace ReHackt.RazorEmails.Configuration
{
    public class SmtpOptions
    {
        public bool EnableSsl { get; set; } = true;

        [Required]
        public string Host { get; set; }

        public string Password { get; set; }

        [Required]
        public int Port { get; set; }

        public string Username { get; set; }
    }
}

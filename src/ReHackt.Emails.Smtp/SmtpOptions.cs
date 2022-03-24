// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReHackt.Emails
{
    public class SmtpOptions
    {
        [DefaultValue(true)]
        public bool EnableSsl { get; set; } = true;

        [Required]
        public string? Host { get; set; }

        public string? Password { get; set; }

        [Required]
        public int Port { get; set; }

        public string? Username { get; set; }
    }
}

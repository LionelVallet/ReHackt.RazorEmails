// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel.DataAnnotations;

namespace ReHackt.Emails
{
    public class EmailTemplateOptions
    {
        public string? Address { get; set; }

        public string ButtonBackgroundColor { get; set; } = "#7FA5DB";

        public string ButtonTextColor { get; set; } = "#FFFFFF";

        [EmailAddress]
        public string? ContactEmail { get; set; }

        public Link[] FooterLinks { get; set; } = Array.Empty<Link>();

        public string HeaderBackgroundColor { get; set; } = "#203279";

        [DataType(DataType.Url)]
        public string? HeaderLinkUri { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? HeaderLogoUri { get; set; }

        public string HeadlineColor { get; set; } = "#111111";

        public string LinkColor { get; set; } = "#203279";

        public string SupportBackgroundColor { get; set; } = "#D8E8FF";

        public string SupportLinkColor { get; set; } = "#203279";

        [DataType(DataType.Url)]
        public string? SupportLinkUri { get; set; }

        public string TextColor { get; set; } = "#666666";
    }

    public class Link
    {
        public string? Text { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string? Uri { get; set; }
    }
}

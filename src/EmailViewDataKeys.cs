// Copyright (c) Lionel Vallet. All rights reserved.
// Licensed under the Apache License, Version 2.0.

namespace ReHackt.RazorEmails
{
    public static partial class EmailViewDataKeys
    {
        public const string Prefix = "Email";

        /// <summary>
        /// Displays the "Contact support" block.
        /// </summary>
        public const string DisplaySupport = Prefix + nameof(DisplaySupport);

        /// <summary>
        /// Explanation of why the user receives this email, regardless of his subscription.
        /// </summary>
        public const string PermissionReminder = Prefix + nameof(PermissionReminder);

        /// <summary>
        /// Preview for some email clients (hidden when the email is fully displayed).
        /// </summary>
        public const string Preview = Prefix + nameof(Preview);

        /// <summary>
        /// Title of the email.
        /// </summary>
        public const string Title = Prefix + nameof(Title);

        /// <summary>
        /// Unsubscribe URL for this kind of email.
        /// </summary>
        public const string UnsubscribeUrl = Prefix + nameof(UnsubscribeUrl);
    }
}

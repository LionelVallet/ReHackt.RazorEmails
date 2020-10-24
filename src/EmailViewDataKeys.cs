namespace ReHackt.RazorEmails
{
    public static partial class EmailViewDataKeys
    {
        /// <summary>
        /// Displays the "Contact support" block.
        /// </summary>
        public const string DisplaySupport = "Email" + nameof(DisplaySupport);

        /// <summary>
        /// Explanation of why the user receives this email, regardless of his subscription.
        /// </summary>
        public const string PermissionReminder = "Email" + nameof(PermissionReminder);

        /// <summary>
        /// Preview for some email clients (hidden when the email is fully displayed).
        /// </summary>
        public const string Preview = "Email" + nameof(Preview);

        /// <summary>
        /// Title of the email.
        /// </summary>
        public const string Title = "Email" + nameof(Title);

        /// <summary>
        /// Unsubscribe URL for this kind of email.
        /// </summary>
        public const string UnsubscribeUrl = "Email" + nameof(UnsubscribeUrl);
    }
}

namespace Labo03_NamedOptions
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public const string GmailSectionName = "Gmail";
        public const string OutlookSectionName = "Outlook";
    }
}

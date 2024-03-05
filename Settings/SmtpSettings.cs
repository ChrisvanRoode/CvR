public class SmtpSettings : ISmtpSettings
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required int Port { get; set; }
    public required bool EnableSsl { get; set; }
}

// ISmtpSettings.cs
public interface ISmtpSettings
{
    string Username { get; set; }
    string Password { get; set; }
    int Port { get; set; }
    bool EnableSsl { get; set; }
}
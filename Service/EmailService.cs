using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;
    private SmtpClient smtpClient;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
        this.smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = _smtpSettings.Port,
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSsl,
        };
    }

    public void SendEmail(string to, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("cbvroode.business@gmail.com"),
            Subject = "subject",
            Body = "<h1>Hello "+to+"</h1></br>"+body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add("cbvroode@gmail.com");
        smtpClient.Send(mailMessage);
    }
}

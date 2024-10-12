using System.Net;
using System.Net.Mail;
using api.interfaces;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    private readonly string _fromEmail;

    public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass, string fromEmail)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPass = smtpPass;
        _fromEmail = fromEmail;
    }

    public async Task SendPasswordResetEmail(string toEmail, string resetLink)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromEmail),
            Subject = "Password Reset Request",
            Body = $"<p>You can reset your password by clicking the link below:</p><p><a href='{resetLink}'>Reset Password</a></p>",
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

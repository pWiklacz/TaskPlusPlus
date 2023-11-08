using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using TaskPlusPlus.Application.Contracts.Infrastructure;
using TaskPlusPlus.Application.Models.Mail;

namespace TaskPlusPlus.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _mailSettings;
    public EmailSender(IOptions<EmailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }
    public async Task<bool> SendEmailAsync(Application.Models.Mail.Email email)
    {
        try
        {
            using var emailMessage = new MimeMessage();
            var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            emailMessage.From.Add(emailFrom);
            var emailTo = new MailboxAddress(email.RecipientName, email.To);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = email.Subject;

            var emailBodyBuilder = new BodyBuilder();

            if (!email.IsHtml)
                emailBodyBuilder.TextBody = email.Body;
            else
                emailBodyBuilder.HtmlBody = email.Body;

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            using var mailClient = new SmtpClient();
            await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
            await mailClient.SendAsync(emailMessage);
            await mailClient.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            
            return false;
        }
    }
}
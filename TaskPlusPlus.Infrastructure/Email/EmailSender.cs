using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using TaskPlusPlus.Application.Contracts.Infrastructure;
using TaskPlusPlus.Application.Models;

namespace TaskPlusPlus.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _mailSettings;
    public EmailSender(IOptions<EmailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }
    public async Task<bool> SendEmail(Application.Models.Email email)
    {
        try
        {
            using var emailMessage = new MimeMessage();
            var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            emailMessage.From.Add(emailFrom);
            var emailTo = new MailboxAddress(email.RecipientName, email.To);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = email.Subject;

            var emailBodyBuilder = new BodyBuilder
            {
                TextBody = email.Body
            };

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
            // Exception Details
            return false;
        }
    }
}
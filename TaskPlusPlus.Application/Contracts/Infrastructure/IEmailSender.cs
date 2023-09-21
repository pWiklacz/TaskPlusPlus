using TaskPlusPlus.Application.Models.Mail;

namespace TaskPlusPlus.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}
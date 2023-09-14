using TaskPlusPlus.Application.Models;

namespace TaskPlusPlus.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}
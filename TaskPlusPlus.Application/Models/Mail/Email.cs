namespace TaskPlusPlus.Application.Models.Mail;

public class Email
{
    public string To { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string Subject { get; set; } = string.Empty;
    public string? Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = false;
}
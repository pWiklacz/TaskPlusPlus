using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Models.Identity.ApplicationUser;
public class UserSettings
{
    public string Theme { get; set; } = "pulse";
    public string StartPage { get; set; } = "Inbox";
    public string Language { get; set; } = "ENG";
    public string TimeFormat { get; set; } = "12";
    public string DateFormat { get; set; } = "DD-MM-YYYY";
    public CategorySettings InboxSettings { get; set; } = new();
    public CategorySettings TodaySettings { get; set; } = new();
    public CategorySettings NextActionsSettings { get; set; } = new();
    public CategorySettings WaitingForSettings { get; set; } = new();
    public CategorySettings SomedaySettings { get; set; } = new();
}

using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;
using Task = System.Threading.Tasks.Task;

namespace TaskPlusPlus.Persistence.Context;
public class TaskPlusPlusDbContextSeed
{
    public static async Task SeedAsync(TaskPlusPlusDbContext context)
    {
        if (!context.Categories.Any())
        {

            var settings = new CategorySettings
            {
                Direction = false,
                Grouping = "None",
                Sorting = "Name"
            };

            var inbox = Category.Create("Inbox", true, false, "#0000FF", "SystemCategory", settings);

            var nextActions = Category.Create("Next Actions", true, false, "#0000FF", "SystemCategory", settings);

            var calendar = Category.Create("Calendar", true, false, "#0000FF", "SystemCategory", settings);

            var project = Category.Create("Project", true, false, "#0000FF", "SystemCategory", settings);

            var waitingFor = Category.Create("Waiting For", true, false, "#0000FF", "SystemCategory", settings);

            var someday = Category.Create("Someday/Maybe", true, false, "#0000FF", "SystemCategory", settings);

            context.Categories.AddRange(inbox.Value, nextActions.Value, calendar.Value, project.Value, waitingFor.Value, someday.Value);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}

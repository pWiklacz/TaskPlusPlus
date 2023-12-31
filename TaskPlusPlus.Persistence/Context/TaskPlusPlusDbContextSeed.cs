﻿using TaskPlusPlus.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskPlusPlus.Persistence.Context;
public class TaskPlusPlusDbContextSeed
{
    public static async Task SeedAsync(TaskPlusPlusDbContext context)
    {
        if (!context.Categories.Any())
        {
            var inbox = Category.Create("Inbox", true, false, "#0000FF", "SystemCategory", "fa-solid fa-inbox");

            var nextActions = Category.Create("Next Actions", true, false, "#0000FF", "SystemCategory", "fa-solid fa-angles-right");

            var calendar = Category.Create("Calendar", true, false, "#0000FF", "SystemCategory", "fa-solid fa-calendar");

            var project = Category.Create("Project", true, false, "#0000FF", "SystemCategory", "fa-solid fa-list-check");

            var waitingFor = Category.Create("Waiting For", true, false, "#0000FF", "SystemCategory", "fa-solid fa-hourglass-half");

            var someday = Category.Create("Someday/Maybe", true, false, "#0000FF", "SystemCategory", "fa-solid fa-lightbulb");

            context.Categories.AddRange(inbox.Value, nextActions.Value, calendar.Value, project.Value, waitingFor.Value, someday.Value);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskPlusPlus.Identity;
using TaskPlusPlus.Identity.Models;
using TaskPlusPlus.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

await UpdateDataBase(app);

app.Run();

async Task UpdateDataBase(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskPlusPlusDbContext>();
    var identityContext = services.GetRequiredService<TaskPlusPlusIdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var logger = services.GetRequiredService<ILogger<Program>>();

//TODO:: use serilog here

    try
    {
        await context.Database.MigrateAsync();
        await identityContext.Database.MigrateAsync();
        //TODO:: SeedDatabaseHere
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured during migration");
    }
}



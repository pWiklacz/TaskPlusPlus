using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskPlusPlus.API.Extensions;
using TaskPlusPlus.Identity;
using TaskPlusPlus.Identity.Models;
using TaskPlusPlus.Persistence;
using TaskPlusPlus.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

app.UseCors("CorsPolicy");
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
        await TaskPlusPlusDbContextSeed.SeedAsync(context);

    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during migration");
    }
}



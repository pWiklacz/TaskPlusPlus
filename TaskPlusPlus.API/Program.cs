using Azure.Identity;
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

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
}

var app = builder.Build();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

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



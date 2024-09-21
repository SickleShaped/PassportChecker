using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace PassportChecker.Extensions;

public static class DatabaseBuilderService
{
    public static void UseDBInitialize(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<ApiDbContext>();
                context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, $"An error occured while initializing the database: {e.Message}");
            }
        }
    }
}

using PassportChecker.Services.Implementations;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Extensions;

public static class DependencyInjectionBuilderService
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager builder)
    {
        services.AddTransient<IPassportService, PassportPostgreSQLService>();
        services.AddTransient<IDataUpdaterService, DataUpdaterService>();
        return services;
    }
}
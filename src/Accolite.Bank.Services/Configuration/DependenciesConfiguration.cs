using Accolite.Bank.Data.MsSql.Configuration;
using Accolite.Bank.Services.Interfaces.Providers;
using Accolite.Bank.Services.Interfaces.Services;
using Accolite.Bank.Services.Providers;
using Accolite.Bank.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Accolite.Bank.Services.Configuration;

public static class DependenciesConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountsService, AccountsService>();

        return services;
    }

    public static IServiceCollection RegisterProviders(this IServiceCollection services, string connectionString)
    {
        services.RegisterRepositories(connectionString);

        services.AddScoped<IAccountsProvider, AccountsProvider>();
        services.AddScoped<IUsersProvider, UsersProvider>();

        return services;
    }
}

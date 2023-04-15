using Accolite.Bank.Data.MsSql.DbContext;
using Accolite.Bank.Data.MsSql.Interfaces.Repositories;
using Accolite.Bank.Data.MsSql.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accolite.Bank.Data.MsSql.Configuration;

public static class DependenciesConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services, string connectionString)
    {
        services.RegisterDbContext(connectionString);

        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    private static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AccoliteBankContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}

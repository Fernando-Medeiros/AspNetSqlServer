using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence;

public static class PersistenceServiceExtension
{
    public static void ConfigureDatabase(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>((provider, opt) =>
        {
            opt.UseSqlServer(
                configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("WebAPI"));
        });
    }
}

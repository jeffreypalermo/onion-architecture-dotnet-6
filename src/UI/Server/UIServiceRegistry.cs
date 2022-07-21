using Lamar;
using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class UiServiceRegistry : ServiceRegistry
{
    public UiServiceRegistry()
    {
        this.AddScoped<DbContext, DataContext>();
        this.AddDbContextFactory<DataContext>();
        this.AddDbContextFactory<DbContext>();

        Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssemblyContainingType<Core.HealthCheck>();
            scanner.AssemblyContainingType<DataAccess.HealthCheck>();
            scanner.AssemblyContainingType<Api.HealthCheck>();
            scanner.AssemblyContainingType<HealthCheck>();
        });

        this.AddHealthChecks()
            .AddCheck<Core.HealthCheck>("Core")
            .AddCheck<DataAccess.HealthCheck>("DataAccess")
            .AddCheck<HealthCheck>("Server")
            .AddCheck<Api.HealthCheck>("API");
    }
}
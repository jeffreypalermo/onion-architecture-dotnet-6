using Lamar;
using LamarCodeGeneration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Startup;

public class StartupServiceRegistry : ServiceRegistry
{
    public StartupServiceRegistry()
    {
        this.AddScoped<DbContext, DataContext>();
        this.AddDbContextFactory<DataContext>();
        this.AddDbContextFactory<DbContext>();
        

        Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssemblyContainingType<HealthCheck>();
            scanner.AssemblyContainingType<DataAccess.HealthCheck>();
            scanner.AssemblyContainingType<Server.HealthCheck>();
        });

        this.AddHealthChecks().AddCheck<Core.HealthCheck>("Core").AddCheck<DataAccess.HealthCheck>("DataAccess")
            .AddCheck<Server.HealthCheck>("Server").AddCheck<Startup.HealthCheck>("Startup");
    }
}
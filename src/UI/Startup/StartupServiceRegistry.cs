using Lamar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingWithPalermo.ChurchBulletin.Core;
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
    }
}
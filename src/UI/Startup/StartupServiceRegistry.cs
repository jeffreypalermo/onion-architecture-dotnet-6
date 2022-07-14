using Lamar;
using LamarCodeGeneration.Util;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Startup;

public class StartupServiceRegistry : ServiceRegistry
{
    public StartupServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssemblyContainingType<Core.HealthCheck>();
            scanner.AssemblyContainingType<DataAccess.HealthCheck>();
            scanner.AssemblyContainingType<Server.HealthCheck>();
        });       
    }
}
using Lamar;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Startup;

public class StartupServiceRegistry : ServiceRegistry
{
    public StartupServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.AssemblyContainingType<Core.HealthCheck>();
            scanner.AssemblyContainingType<DataAccess.HealthCheck>();
            scanner.LookForRegistries();
        });       
    }
}
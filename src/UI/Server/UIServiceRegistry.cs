using Lamar;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class UIServiceRegistry : ServiceRegistry
{
    public UIServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssemblyContainingType<Startup.HealthCheck>();
            scanner.AssemblyContainingType<HealthCheck>();
            scanner.LookForRegistries();
        });
    }
}
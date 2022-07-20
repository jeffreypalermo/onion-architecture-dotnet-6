using Lamar;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class UiServiceRegistry : ServiceRegistry
{
    public UiServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.WithDefaultConventions();
            scanner.AssembliesFromApplicationBaseDirectory(
                assembly => assembly.FullName!.Contains("UI.Startup"));
            scanner.LookForRegistries();
        });
    }
}
using Lamar;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class UIServiceRegistry : ServiceRegistry
{
    public UIServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.AssembliesFromApplicationBaseDirectory(assembly =>
                assembly.FullName!.Contains("UI.Startup"));
            scanner.LookForRegistries();
        });
    }
}
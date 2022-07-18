using System.Reflection;
using Lamar;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class UIServiceRegistry : ServiceRegistry
{
    public UIServiceRegistry(Assembly assembly)
    {
        
        Scan(scanner =>
        {
            scanner.Assembly(assembly);
            
            scanner.LookForRegistries();
        });
    }
}
using Lamar;
using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class InterfaceServiceRegistry : ServiceRegistry
{
    public InterfaceServiceRegistry()
    {
        Console.WriteLine(this);
        
        this.AddControllersWithViews();
        this.AddRazorPages();
        this.AddTransient<IDataConfiguration, DataConfiguration>();
    }
}
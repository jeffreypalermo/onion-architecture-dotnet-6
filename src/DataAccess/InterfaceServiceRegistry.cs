using Lamar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess;

public class InterfaceServiceRegistry : ServiceRegistry
{
    public InterfaceServiceRegistry()
    {
        Console.WriteLine(this);
        
        this.AddTransient<IChurchBulletinItemByDateHandler, ChurchBulletinItemByDateHandler>();
        this.AddScoped<DbContext, DataContext>();
        this.AddDbContextFactory<DataContext>();
        this.AddDbContextFactory<DbContext>();
    }
}
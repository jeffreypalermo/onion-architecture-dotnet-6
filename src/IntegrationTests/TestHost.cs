using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammingWithPalermo.ChurchBulletin.Core;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public static class TestHost
{
    private static bool _dependenciesRegistered;
    private static readonly object Lock = new();
    private static IHost? _host;

    public static IHost Instance
    {
        get
        {
            EnsureDependenciesRegistered();
            return _host!;
        }
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        var serviceScope = Instance.Services.CreateScope();
        var provider = serviceScope.ServiceProvider;
        return provider.GetRequiredService<T>();
    }

    private static void Initialize()
    {
        var host = Host.CreateDefaultBuilder()
            .UseEnvironment("Development")
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;

                config
                    .AddJsonFile("appsettings.test.json", false, true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                s.AddTransient<IDatabaseConfiguration, TestDatabaseConfiguration>();
                s.AddTransient<IChurchBulletinItemByDateHandler, ChurchBulletinItemByDateHandler>();
                s.AddTransient<ChurchBulletinItemByDateHandler>();
                s.AddScoped<DbContext, DataContext>();
                s.AddDbContextFactory<DataContext>();
                s.AddDbContextFactory<DbContext>();
            })
            .Build();


        _host = host;
    }

    private static void EnsureDependenciesRegistered()
    {
        if (!_dependenciesRegistered)
            lock (Lock)
            {
                if (!_dependenciesRegistered)
                {
                    Initialize();
                    _dependenciesRegistered = true;
                }
            }
    }
}
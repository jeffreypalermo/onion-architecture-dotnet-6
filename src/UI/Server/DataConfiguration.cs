using ProgrammingWithPalermo.ChurchBulletin.Core;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class DatabaseConfiguration : IDatabaseConfiguration
{
    private readonly IConfiguration _configuration;

    public DatabaseConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("SqlConnectionString");
    }
}
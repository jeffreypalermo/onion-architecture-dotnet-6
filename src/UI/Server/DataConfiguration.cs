using ProgrammingWithPalermo.ChurchBulletin.DataAccess;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Server;

public class DataConfiguration : IDataConfiguration
{
    private IConfiguration _configuration;

    public DataConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("SqlConnectionString");
    }
}
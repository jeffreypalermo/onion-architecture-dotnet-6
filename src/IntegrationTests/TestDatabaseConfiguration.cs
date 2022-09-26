using Microsoft.Extensions.Configuration;
using ProgrammingWithPalermo.ChurchBulletin.Core;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public class TestDatabaseConfiguration : IDatabaseConfiguration
{
    private readonly IConfiguration _configuration;

    public TestDatabaseConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("SqlConnectionString");
    }
}
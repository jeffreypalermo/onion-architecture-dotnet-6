using Microsoft.Extensions.Configuration;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public class TestDataConfiguration : IDataConfiguration
{
    private readonly IConfiguration _configuration;

    public TestDataConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("SqlConnectionString");
    }
}
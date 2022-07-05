using ProgrammingWithPalermo.ChurchBulletin.DataAccess;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public class TestDataConfiguration : IDataConfiguration
{

    public TestDataConfiguration()
    {
    }

    public string GetConnectionString()
    {
        return "server=(LocalDb)\\MSSQLLocalDB;database=ChurchBulletin;Integrated Security=true;";
    }
}
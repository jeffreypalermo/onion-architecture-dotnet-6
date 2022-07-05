using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public class SqlExecuter
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
    public void ExecuteSql(string commandText, Action<DbDataReader> readerAction)
    {
        var context = new DataContext(new TestDataConfiguration());
        var dbConnection = context.Database.GetDbConnection();

        dbConnection.Open();
        using (var command = dbConnection.CreateCommand())
        {
            command.CommandType = CommandType.Text;
            command.CommandText =
                commandText;
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                readerAction(reader);
            }
        }
        dbConnection.Close();

    }
}
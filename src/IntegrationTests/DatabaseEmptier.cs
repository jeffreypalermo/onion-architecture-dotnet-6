using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public sealed class DatabaseEmptier
{
    private static readonly string[] _ignoredTables = new[] { "[dbo].[sysdiagrams]", "[dbo].[usd_AppliedDatabaseScript]" };
    private static string _deleteSql;

    private class Relationship
    {
        public string PrimaryKeyTable { get; set; }
        public string ForeignKeyTable { get; set; }
    }

    public void DeleteAllData()
    {
        if (_deleteSql == null)
        {
            _deleteSql = BuildDeleteTableSqlStatement();
        }

        var context = new DataContext(new TestDataConfiguration());

        context.Database.ExecuteSqlRaw(_deleteSql);
    }

    private string BuildDeleteTableSqlStatement()
    {
        IList<string> allTables = GetAllTables();
        IList<Relationship> allRelationships = GetRelationships();
        string[] tablesToDelete = BuildTableList(allTables, allRelationships);

        return BuildTableSql(tablesToDelete);
    }

    private static string BuildTableSql(IEnumerable<string> tablesToDelete)
    {
        string completeQuery = "";
        foreach (string tableName in tablesToDelete)
        {
            completeQuery += String.Format("delete from {0};", tableName);
        }
        return completeQuery;
    }

    private static string[] BuildTableList(ICollection<string> allTables, ICollection<Relationship> allRelationships)
    {
        var tablesToDelete = new List<string>();

        while (allTables.Any())
        {
            string[] leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

            if (leafTables.Length == 0)
            {
                tablesToDelete.AddRange(allTables);
                break;
            }

            tablesToDelete.AddRange(leafTables);

            foreach (string leafTable in leafTables)
            {
                allTables.Remove(leafTable);
                Relationship[] relToRemove =
                    allRelationships.Where(rel => rel.ForeignKeyTable == leafTable).ToArray();
                foreach (Relationship rel in relToRemove)
                {
                    allRelationships.Remove(rel);
                }
            }
        }

        return tablesToDelete.ToArray();
    }

    private static IList<Relationship> GetRelationships()
    {
        var relationships = new List<Relationship>();
        new SqlExecuter().ExecuteSql(
            @"select
	'[' + ss_pk.name + '].[' + so_pk.name + ']' as PrimaryKeyTable
, '[' + ss_fk.name + '].[' + so_fk.name + ']' as ForeignKeyTable
from
	sysforeignkeys sfk
	  inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
	  inner join sys.tables st_pk on so_pk.id = st_pk.object_id
	  inner join sys.schemas ss_pk on st_pk.schema_id = ss_pk.schema_id
	  inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
	  inner join sys.tables st_fk on so_fk.id = st_fk.object_id
	  inner join sys.schemas ss_fk on st_fk.schema_id = ss_fk.schema_id
order by
	so_pk.name
,   so_fk.name;",
            reader => relationships.Add(
                new Relationship()
                {
                    PrimaryKeyTable = reader["PrimaryKeyTable"].ToString(),
                    ForeignKeyTable = reader["ForeignKeyTable"].ToString()
                }));

        return relationships;
    }

    private static IList<string> GetAllTables()
    {
        List<string> tables = new List<string>();

        new SqlExecuter().ExecuteSql(
            @"select '[' + s.name + '].[' + t.name + ']'
from sys.tables t
inner join sys.schemas s on t.schema_id = s.schema_id",
            reader => tables.Add(reader.GetString(0)));
        var list = tables.Except(_ignoredTables);
        return list.Where(s => s.Contains("[dbo]")).ToList();
    }
}

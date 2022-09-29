using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

[TestFixture]
public class ZDataLoader
{
    [Test]
    public void LoadData()
    {
        new DatabaseEmptier(TestHost.GetRequiredService<DbContext>().Database).DeleteAllData();

        var item1 = new ChurchBulletinItem {Date = new DateTime(2000, 1, 1), Name = "one", Place = "Sanctuary"};
        var item2 = new ChurchBulletinItem {Date = new DateTime(2000, 1, 1), Name = "two", Place = "Room 205"};
        var item3 = new ChurchBulletinItem {Date = new DateTime(2000, 1, 1), Name = "three", Place = "Nursery"};
        var item4 = new ChurchBulletinItem {Date = new DateTime(2000, 1, 1), Name = "four", Place = "Youth room"};

        using (var context = TestHost.GetRequiredService<DbContext>())
        {
            context.AddRange(item1, item2, item3, item4);
            context.SaveChanges();
        }

        Assert.Pass();
    }
}
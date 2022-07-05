using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;
using Shouldly;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

[TestFixture]
public class ChurchBulletinItemByDateQueryTester
{
    [Test]
    public void ShouldGetWithinDate()
    {
        EmptyDatabase();
        var item1 = new ChurchBulletinItem() { Date = new DateTime(2000, 1, 1) };
        var item2 = new ChurchBulletinItem() { Date = new DateTime(1999, 1, 1) };
        var item3 = new ChurchBulletinItem() { Date = new DateTime(2001, 1, 1) };
        var item4 = new ChurchBulletinItem() { Date = new DateTime(2000, 1, 1) };

        using (var context = new DataContext(new TestDataConfiguration()))
        {
            context.AddRange(item1, item2, item3, item4);
            context.SaveChanges();
        }

        //arrange
        var query = new ChurchBulletinItemByDateAndTimeQuery(new DateTime(2000, 1, 1));
        var handler = new ChurchBulletinItemByDateHandler(new DataContext(new TestDataConfiguration()));

        //act
        IEnumerable<ChurchBulletinItem> items = handler.Handle(query).ToList();

        //assert
        items.Count().ShouldBe(2);
        items.ShouldContain(item1);
        items.ShouldContain(item4);
        items.ShouldNotContain(item2);
        items.ShouldNotContain(item3);
    }

    private void EmptyDatabase()
    {
        new DatabaseEmptier().DeleteAllData();
    }
}
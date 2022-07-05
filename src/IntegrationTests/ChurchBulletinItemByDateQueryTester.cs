using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

[TestFixture]
public class ChurchBulletinItemByDateQueryTester
{
    [Test]
    public void ShouldGetWithinDate()
    {
        var query = new ChurchBulletinItemByDateAndTimeQuery(new DateTime(1, 1, 2000));
        var handler = new ChurchBulletinItemByDateHandler();
        handler.Handle(query);
    }
}
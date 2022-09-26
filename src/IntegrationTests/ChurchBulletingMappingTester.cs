using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using Shouldly;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

public class ChurchBulletingMappingTester
{
    [Test]
    public void ShouldMapChurchBulletin()
    {
        var bulletin = new ChurchBulletinItem();
        bulletin.Name = "Worship service";
        bulletin.Place = "Sanctuary";
        bulletin.Date = new DateTime(2022, 1, 1);

        using (var context = TestHost.GetRequiredService<DbContext>())
        {
            context.Add(bulletin);
            context.SaveChanges();
        }

        ChurchBulletinItem rehydratedEntity;
        using (var context = TestHost.GetRequiredService<DbContext>())
        {
            rehydratedEntity = context.Set<ChurchBulletinItem>()
                .Single(b => b == bulletin);
        }

        rehydratedEntity.Id.ShouldBe(bulletin.Id);
        rehydratedEntity.ShouldBe(bulletin);
        rehydratedEntity.Name.ShouldBe(bulletin.Name);
        rehydratedEntity.Place.ShouldBe(bulletin.Place);
        rehydratedEntity.Date.ShouldBe(bulletin.Date);
    }
}
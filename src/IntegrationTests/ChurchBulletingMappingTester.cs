using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;
using Shouldly;

namespace ProgrammingWithPalermo.ChurchBulletin.IntegrationTests
{
    public class ChurchBulletingMappingTester
    {
        [Test]
        public void ShouldMapChurchBulletin()
        {
            var bulletin = new ChurchBulletinItem();
            bulletin.Name = "Worship service";
            bulletin.Place = "Sanctuary";
            bulletin.Date = new DateTime(2022, 1, 1);

            using (var context = new DataContext(new TestDataConfiguration()))
            {
                context.Add(bulletin);
                context.SaveChanges();
            }

            ChurchBulletinItem? rehydratedEntity;
            using (var context = new DataContext(new TestDataConfiguration()))
            {
                 rehydratedEntity = context.Set<ChurchBulletinItem>()
                    .SingleOrDefault(b => b == bulletin);
            }

            rehydratedEntity.Id.ShouldBe(bulletin.Id);
            rehydratedEntity.ShouldBe(bulletin);
            rehydratedEntity.Name.ShouldBe(bulletin.Name);
            rehydratedEntity.Place.ShouldBe(bulletin.Place);
            rehydratedEntity.Date.ShouldBe(bulletin.Date);
        }
    }
}
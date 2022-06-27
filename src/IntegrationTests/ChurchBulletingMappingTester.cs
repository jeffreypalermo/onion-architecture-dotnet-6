using DataAccess.Mappings;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using Shouldly;

namespace IntegrationTests
{
    public class ChurchBulletingMappingTester
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldMapChurchBulletin()
        {
            var bulletin = new ChurchBulletin();
            bulletin.Name = "Worship service";
            bulletin.Place = "Sanctuary";
            bulletin.Date = new DateTime(2022, 1, 1);

            using (var context = new DataContext(new TestDataConfiguration()))
            {
                context.Add(bulletin);
                context.SaveChanges();
            }

            ChurchBulletin? rehydratedEntity;
            using (var context = new DataContext(new TestDataConfiguration()))
            {
                 rehydratedEntity = context.Set<ChurchBulletin>()
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
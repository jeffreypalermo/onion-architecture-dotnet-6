using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using Shouldly;

namespace ProgrammingWithPalermo.ChurchBulletin.UnitTests
{
    public class ChurchBulletinItemTester
    {
        [Test]
        public void ShouldHaveEntityEqualitySemantics()
        {
            new ChurchBulletinItem { Id = Guid.NewGuid() }
                .ShouldNotBe(new ChurchBulletinItem { Id = Guid.NewGuid() });

            var item1 = new ChurchBulletinItem();
            var item2 = new ChurchBulletinItem();
            item1.Id = Guid.NewGuid();
            item2.Id = item1.Id;
            item1.ShouldBe(item2);
            item2.ShouldBe(item1);
            Assert.True(item1 == item2);
        }

        [Test]
        public void ShouldOutputFriendlyPlace()
        {
            var item = new ChurchBulletinItem(){Place = "Sanctuary"};
            item.GetFriendlyPlace().ShouldBe("@ Sanctuary");
        }
    }
}
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;

namespace ProgrammingWithPalermo.ChurchBulletin.Core.Queries;

public interface IChurchBulletinItemByDateHandler
{
    IEnumerable<ChurchBulletinItem> Handle(ChurchBulletinItemByDateAndTimeQuery query);
}
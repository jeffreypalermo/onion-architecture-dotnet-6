using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;

public class ChurchBulletinItemByDateHandler : IChurchBulletinItemByDateHandler
{
    private readonly DataContext _context;

    public ChurchBulletinItemByDateHandler(DataContext context)
    {
        _context = context;
    }
    public IEnumerable<ChurchBulletinItem> Handle(ChurchBulletinItemByDateAndTimeQuery query)
    {
        var items = _context.Set<ChurchBulletinItem>()
            .Where(item => item.Date == query.TargetDate).AsEnumerable();
        return items;
    }
}
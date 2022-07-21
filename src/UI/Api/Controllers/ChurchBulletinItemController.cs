using Microsoft.AspNetCore.Mvc;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;

namespace ProgrammingWithPalermo.ChurchBulletin.UI.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChurchBulletinItemController : ControllerBase
{
    private readonly IChurchBulletinItemByDateHandler _handler;

    public ChurchBulletinItemController(IChurchBulletinItemByDateHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public IEnumerable<ChurchBulletinItem> Get()
    {
        IEnumerable<ChurchBulletinItem> items = _handler.Handle(
            new ChurchBulletinItemByDateAndTimeQuery(new DateTime(2000, 1,1)));
        return items;
    }
}
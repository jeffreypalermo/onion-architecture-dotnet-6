using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

public class ChurchBulletinMap : EntityMapBase<ChurchBulletinItem>
{
    protected override void MapMembers(EntityTypeBuilder<ChurchBulletinItem> entity)
    {
        
    }
}
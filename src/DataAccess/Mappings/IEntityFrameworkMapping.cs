using Microsoft.EntityFrameworkCore;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

public interface IEntityFrameworkMapping
{
    void Map(ModelBuilder builder);
}
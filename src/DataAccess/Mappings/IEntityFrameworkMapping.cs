using Microsoft.EntityFrameworkCore;

namespace DataAccess.Mappings;

public interface IEntityFrameworkMapping
{
    void Map(ModelBuilder builder);
}
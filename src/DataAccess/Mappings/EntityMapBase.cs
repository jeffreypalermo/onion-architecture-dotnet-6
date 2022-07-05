using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;

namespace ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;

public abstract class EntityMapBase<T> : IEntityFrameworkMapping where T : EntityBase<T>, new()
{
    public void Map(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<T>(entity =>
        {
            entity.ToTable(typeof(T).Name, "dbo");
            entity.UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired()
                .HasValueGenerator<SequentialGuidValueGenerator>()
                .ValueGeneratedOnAdd()
                .HasDefaultValue(Guid.Empty);

            MapMembers(entity);
        });
    }

    protected abstract void MapMembers(EntityTypeBuilder<T> entity);
}
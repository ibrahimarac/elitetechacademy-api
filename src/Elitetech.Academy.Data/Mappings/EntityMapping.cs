using Elitetech.Academy.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elitetech.Academy.Data.Mappings
{
    public abstract class EntityMapping<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public abstract void ConfigureDerivedEntity(EntityTypeBuilder<T> builder);

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnOrder(1);
            builder.HasKey(x => x.Id);

            //Diğer entity'den gelen propertyler gelir
            ConfigureDerivedEntity(builder);

            builder.Property(x => x.CreatedUser).HasColumnName("CreatedUser").HasColumnOrder(30);
            builder.Property(x => x.UpdatedUser).HasColumnName("UpdatedUser").HasColumnOrder(31);
            builder.Property(x => x.CreatedTime).HasColumnName("CreatedTime").HasColumnOrder(32);
            builder.Property(x => x.UpdatedTime).HasColumnName("UpdatedTime").HasColumnOrder(33);
        }
    }
}

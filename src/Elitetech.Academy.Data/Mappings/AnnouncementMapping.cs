using Elitetech.Academy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elitetech.Academy.Data.Mappings
{
    public class AnnouncementMapping : EntityMapping<Announcement>
    {
        public override void ConfigureDerivedEntity(EntityTypeBuilder<Announcement> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Title").HasColumnType("nvarchar(150)").HasColumnOrder(2);

            builder.Property(x => x.Detail).HasColumnName("Detail").HasColumnType("nvarchar(8000)").HasColumnOrder(3);

            builder.Property(x => x.SendNotification).HasColumnName("SendNotification").HasColumnOrder(4);

            builder.Property(x => x.SendSms).HasColumnName("SendSms").HasColumnOrder(5);

            builder.Property(x => x.StartDate).HasColumnName("StartDate").HasColumnOrder(6);

            builder.Property(x => x.EndDate).HasColumnName("EndDate").HasColumnOrder(7);

            builder.ToTable("Announcements");
        }
    }
}

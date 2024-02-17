using Elitetech.Academy.Data.Context;
using Elitetech.Academy.Domain.Entities;
using Elitetech.Academy.Domain.Repository;

namespace Elitetech.Academy.Data.Repository
{
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(EliteContext context) : base(context) { }
    }
}

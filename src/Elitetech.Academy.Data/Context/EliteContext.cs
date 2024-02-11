using Elitetech.Academy.Data.Mappings;
using Elitetech.Academy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elitetech.Academy.Data.Context
{
    public class EliteContext : DbContext
    {
        public EliteContext(DbContextOptions<EliteContext> options) : base(options)
        {

        }

        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnnouncementMapping());
        }
    }
}

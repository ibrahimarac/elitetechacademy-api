using Elitetech.Academy.InfraPack.Extensions;

namespace Elitetech.Academy.Domain.Entities.Base
{
    public class Entity
    {
        public Entity()
        {
            CreatedTime = DateTime.UtcNow.ToTurkeyLocalTime();
            IsDeleted = false;
            CreatedUser = "admin";
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        
    }
}

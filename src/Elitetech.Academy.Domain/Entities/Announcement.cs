using Elitetech.Academy.Domain.Entities.Base;
using Elitetech.Academy.Domain.Enumerations;

namespace Elitetech.Academy.Domain.Entities
{
    public class Announcement : Entity
    {
        public string Title { get; set; }
        public string Detail { get; set; } //update edilebilir
        public bool SendNotification { get; set; } //update edilebilir (false ise true yapılabilir)
        public bool SendSms { get; set; } //update edilebilir (false ise true yapılabilir)
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } //update edilebilir
        public AnnouncementStatus AnnouncementStatus { get; set; }
    }
}

using Elitetech.Academy.Domain.Entities.Base;

namespace Elitetech.Academy.Domain.Entities
{
    public class Announcement : Entity
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public bool SendNotification { get; set; }
        public bool SendSms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

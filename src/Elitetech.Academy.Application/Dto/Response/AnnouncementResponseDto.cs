namespace Elitetech.Academy.Application.Dto.Response
{
    public class AnnouncementResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public bool SendNotification { get; set; }
        public bool SendSms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

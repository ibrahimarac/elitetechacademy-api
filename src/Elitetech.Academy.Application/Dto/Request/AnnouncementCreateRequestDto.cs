namespace Elitetech.Academy.Application.Dto.Request
{
    public class AnnouncementCreateRequestDto
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

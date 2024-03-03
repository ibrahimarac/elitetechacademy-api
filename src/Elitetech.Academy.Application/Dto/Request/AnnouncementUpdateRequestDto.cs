namespace Elitetech.Academy.Application.Dto.Request
{
    public class AnnouncementUpdateRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

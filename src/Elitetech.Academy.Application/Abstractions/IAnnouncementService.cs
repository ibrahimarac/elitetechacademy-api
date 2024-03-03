using Elitetech.Academy.Application.Dto.Request;
using Elitetech.Academy.Application.Wrapper;

namespace Elitetech.Academy.Application.Abstractions
{
    public interface IAnnouncementService
    {
        Task<Result> GetAllAsync();
        Task<Result> AddAsync(AnnouncementCreateRequestDto announcementCreateRequest);
        Task<Result> UpdateAsync(AnnouncementUpdateRequestDto announcementUpdateRequest);
        Task<Result> SendNotificationAsync(int announcementId);
        Task<Result> SendSmsAsync(int announcementId);
        Task<Result> DeleteAsync(int id);
    }
}

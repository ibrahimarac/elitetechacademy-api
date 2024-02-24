using Elitetech.Academy.Application.Dto.Request;
using Elitetech.Academy.Application.Wrapper;

namespace Elitetech.Academy.Application.Abstractions
{
    public interface IAnnouncementService
    {
        Task<Result> GetAllAsync();
        Task<Result> AddAsync(AnnouncementCreateRequestDto announcementCreateRequest);
        Task<Result> Update(AnnouncementUpdateRequestDto announcementUpdateRequest);
        Task<Result> DeleteAsync(int id);
    }
}

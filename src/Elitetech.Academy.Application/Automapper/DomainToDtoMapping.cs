using AutoMapper;
using Elitetech.Academy.Application.Dto.Response;
using Elitetech.Academy.Domain.Entities;

namespace Elitetech.Academy.Application.Automapper
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Announcement, AnnouncementResponseDto>();
        }
    }
}

using AutoMapper;
using Elitetech.Academy.Application.Dto.Request;
using Elitetech.Academy.Domain.Entities;

namespace Elitetech.Academy.Application.Automapper
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<AnnouncementCreateRequestDto, Announcement>();
        }
    }
}

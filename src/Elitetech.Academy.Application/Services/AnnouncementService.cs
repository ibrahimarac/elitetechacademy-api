using AutoMapper;
using Elitetech.Academy.Application.Abstractions;
using Elitetech.Academy.Application.Dto.Request;
using Elitetech.Academy.Application.Dto.Response;
using Elitetech.Academy.Application.Wrapper;
using Elitetech.Academy.Domain.Entities;
using Elitetech.Academy.Domain.Repository.Base;
using Microsoft.Extensions.Logging;

namespace Elitetech.Academy.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AnnouncementService> _logger;

        public AnnouncementService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AnnouncementService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> GetAllAsync()
        {
            var announcementEntities = await _unitOfWork.AnnouncementRepository.GetAllAsync();
            var announcementVm = _mapper.Map<IEnumerable<AnnouncementResponseDto>>(announcementEntities);
            return Result.Success(announcementVm);
        }

        public async Task<Result> AddAsync(AnnouncementCreateRequestDto announcementCreateRequest)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcementCreateRequest);
            await _unitOfWork.AnnouncementRepository.AddAsync(announcementEntity);

            try
            {
                await _unitOfWork.CommitAsync();
                return Result.Success(announcementEntity.Id, "Duyuru başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Duyuru eklenirken bir hata oluştu.");
                return Result.Error("Kayıt esnasında bir hata oluştu.");
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            #region Model kontrolü

            var existsAnnouncement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(id);
            if (existsAnnouncement is null)
            {
                return Result.NotFound("Duyuru bulunamadı.");
            }

            #endregion

            #region Kayıt işlemi

            try
            {
                _unitOfWork.AnnouncementRepository.Remove(id);
                await _unitOfWork.CommitAsync();
                return Result.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AnnouncementService => DeleteAsync : Duyuru silinemedi.");
                return Result.Error("Duyuru silinemedi.");
            }

            #endregion
        }

        public async Task<Result> Update(AnnouncementUpdateRequestDto announcementUpdateRequest)
        {
            #region Model kontrolü

            var existsAnnouncement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(announcementUpdateRequest.Id);
            if (existsAnnouncement is null)
            {
                return Result.NotFound($"{announcementUpdateRequest.Id} nolu duyuru bulunamadı.");
            }

            #endregion

            #region Kayıt işlemi

            try
            {
                _unitOfWork.AnnouncementRepository.Remove(announcementUpdateRequest.Id);
                await _unitOfWork.CommitAsync();
                return Result.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AnnouncementService => UpdateAsync : Güncelleme esnasında bir hata oluştu.");
                return Result.Error("Kayıt esnasında bir hata oluştu.");
            }

            #endregion
        }
    }
}

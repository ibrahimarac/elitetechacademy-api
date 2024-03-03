using AutoMapper;
using Elitetech.Academy.Application.Abstractions;
using Elitetech.Academy.Application.Aspects;
using Elitetech.Academy.Application.Dto.Request;
using Elitetech.Academy.Application.Dto.Response;
using Elitetech.Academy.Application.Validators;
using Elitetech.Academy.Application.Wrapper;
using Elitetech.Academy.Domain.Entities;
using Elitetech.Academy.Domain.Enumerations;
using Elitetech.Academy.Domain.Repository.Base;
using Elitetech.Academy.InfraPack.Extensions;
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


        [ValidationAspect(typeof(AnnouncementCreateRequestValidator), true)]
        public async Task<Result> AddAsync(AnnouncementCreateRequestDto announcementCreateRequest)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcementCreateRequest);
            await _unitOfWork.AnnouncementRepository.AddAsync(announcementEntity);

            try
            {
                await _unitOfWork.CommitAsync();
                return Result.Success(announcementEntity.Id, "Duyuru başarıyla eklendi.");
            }
            catch (Exception ex) // Exception handling
            {
                _logger.LogError(ex, "Duyuru eklenirken bir hata oluştu.");
                return Result.Error("Kayıt esnasında bir hata oluştu.");
            }
        }


        [ValidationAspect(typeof(AnnouncementUpdateRequestValidator), true)]
        public async Task<Result> UpdateAsync(AnnouncementUpdateRequestDto announcementUpdateRequest)
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


        public async Task<Result> PublishAnnouncementAsync(int announcementId)
        {            
            var existsAnnouncement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(announcementId);
            var date = DateTime.UtcNow.ToTurkeyLocalTime();

            #region Model Kontrolü

            if(existsAnnouncement is null)
            {
                return Result.NotFound("Belirtilen kriterlere uygun duyuru bulunamadı.");
            }
            else if(existsAnnouncement.AnnouncementStatus != AnnouncementStatus.Created)
            {
                return Result.Error("Daha önce yayınlanmış bir duyuru tekrar yayınlanamaz.");
            }
            else if (existsAnnouncement.StartDate < date)
            {
                return Result.Error("Başlangıç tarihi şu anki tarihten ileri bir tarih olmalıdır.");
            }

            #endregion

            #region Kayıt İşlemi

            try
            {
                existsAnnouncement.AnnouncementStatus = AnnouncementStatus.Sent;
                _unitOfWork.AnnouncementRepository.Update(existsAnnouncement);
                await _unitOfWork.CommitAsync();
                return Result.Success(true, "Duyuru başarıyla yayınlandı.");   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AnnouncementService => PublishAnnouncementAsync : Duyuru yayınlanamadı.");
                return Result.Error("Duyuru yayınlanamadı.");
            }

            #endregion
        }


        public async Task<Result> SendNotificationAsync(int announcementId)
        {
            var existsAnnouncement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(announcementId);
            var date = DateTime.UtcNow.ToTurkeyLocalTime();

            #region Model Kontrolü

            if (existsAnnouncement is null)
            {
                return Result.NotFound("Belirtilen kriterlere uygun duyuru bulunamadı.");
            }
            else if (existsAnnouncement.AnnouncementStatus != AnnouncementStatus.Sent)
            {
                return Result.Error("Yayınlanmamış bir duyuru için bildirim gönderilemez.");
            }
            else if (existsAnnouncement.EndDate.HasValue && existsAnnouncement.EndDate < date)
            {
                return Result.Error("Bitiş tarihi geçmiş bir duyuru için bildirim gönderilemez.");
            }
            else if (existsAnnouncement.SendNotification)
            {
                return Result.Error("Bu duyuru için daha önce bildirim gönderilmiştir.");
            }

            #endregion

            #region Kayıt İşlemi

            try
            {
                existsAnnouncement.SendNotification = true;
                _unitOfWork.AnnouncementRepository.Update(existsAnnouncement);
                await _unitOfWork.CommitAsync();

                //todo : Bildirim gönderim işlemleri eklenecek.

                return Result.Success(true, "Bildirim gönderimi tamamlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AnnouncementService => PublishAnnouncementAsync : Duyuru yayınlanamadı.");
                return Result.Error("Duyuru yayınlanamadı.");
            }

            #endregion
        }


        public async Task<Result> SendSmsAsync(int announcementId)
        {
            var existsAnnouncement = await _unitOfWork.AnnouncementRepository.GetByIdAsync(announcementId);
            var date = DateTime.UtcNow.ToTurkeyLocalTime();

            #region Model Kontrolü

            if (existsAnnouncement is null)
            {
                return Result.NotFound("Belirtilen kriterlere uygun duyuru bulunamadı.");
            }
            else if (existsAnnouncement.AnnouncementStatus != AnnouncementStatus.Sent)
            {
                return Result.Error("Yayınlanmamış bir duyuru için sms gönderilemez.");
            }
            else if (existsAnnouncement.EndDate.HasValue && existsAnnouncement.EndDate < date)
            {
                return Result.Error("Bitiş tarihi geçmiş bir duyuru için sms gönderilemez.");
            }
            else if (existsAnnouncement.SendSms)
            {
                return Result.Error("Bu duyuru için daha önce sms gönderilmiştir.");
            }

            #endregion

            #region Kayıt İşlemi

            try
            {
                existsAnnouncement.SendSms = true;
                _unitOfWork.AnnouncementRepository.Update(existsAnnouncement);
                await _unitOfWork.CommitAsync();

                //todo : Sms gönderim işlemleri eklenecek.

                return Result.Success(true, "Bildirim gönderimi tamamlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AnnouncementService => PublishAnnouncementAsync : Duyuru yayınlanamadı.");
                return Result.Error("Duyuru yayınlanamadı.");
            }

            #endregion
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
                
    }
}

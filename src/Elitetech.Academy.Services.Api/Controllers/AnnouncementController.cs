using Elitetech.Academy.Application.Abstractions;
using Elitetech.Academy.Application.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace Elitetech.Academy.Services.Api.Controllers
{
    [Route("announcement")]
    public class AnnouncementController : ApiController
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return CustomResponse(await _announcementService.GetAllAsync());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AnnouncementCreateRequestDto announcementCreateRequest)
        {
            return CustomResponse(await _announcementService.AddAsync(announcementCreateRequest));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(AnnouncementUpdateRequestDto announcementUpdateRequest)
        {
            return CustomResponse(await _announcementService.Update(announcementUpdateRequest));
        }

        [HttpDelete("delete/{announcementId:int}")]
        public async Task<IActionResult> Delete(int announcementId)
        {
            return CustomResponse(await _announcementService.DeleteAsync(announcementId));
        }

    }
}

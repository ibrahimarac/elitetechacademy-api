using Elitetech.Academy.Application.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace Elitetech.Academy.Services.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        protected ActionResult CustomResponse(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else if(result.Status == ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}

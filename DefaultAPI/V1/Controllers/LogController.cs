using AutoMapper;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DefaultAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    [ApiExplorerSettings(GroupName = "1.0")]
    // [ApiExplorerSettings(IgnoreApi = true)] // Ignora completamente os endpoints da controller
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(IMapper mapper, IGeneralService generalService, INotificationMessageService notificationMessageService, ILogService logService) : base(mapper, generalService, notificationMessageService)
        {
            _logService = logService;
        }

        [HttpGet("getById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = _mapperService.Map<LogReturnedDto>(await _logService.GetById(id));

            if (record == null)
            {
                return NotFound();
            }

            return CustomResponse(record);
        }

        [HttpPost("GetAllFilter")]
        public async Task<IActionResult> GetAllFilter(LogFilter filter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _logService.GetAllWithPaginate(filter));
        }
    }
}

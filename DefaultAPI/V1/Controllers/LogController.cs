using AutoMapper;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    // [ApiExplorerSettings(IgnoreApi = true)] // Ignora completamente os endpoints da controller
    public sealed class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(IMapper mapper, IGeneralService generalService, INotificationMessageService notificationMessageService, ILogService logService) : base(mapper, generalService, notificationMessageService)
        {
            _logService = logService;
        }

        [HttpGet("getById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _logService.ExistById(id) == false)
                return CustomResponse();

            var model = _mapperService.Map<UserReturnedDto>(await _logService.GetById(id));

            return CustomResponse(model, Constants.SuccessInGetId);
        }

        [HttpPost("GetAllPaginate")]
        public async Task<IActionResult> GetAllPaginate(LogFilter filter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var model = await _logService.GetAllPaginate(filter);

            return CustomResponse(model, Constants.SuccessInGetAllPaginate);
        }
    }
}

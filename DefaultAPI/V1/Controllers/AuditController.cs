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
    public sealed class AuditController : BaseController
    {
        private readonly IAuditService _auditService;

        public AuditController(IMapper mapper, IGeneralService generalService, INotificationMessageService noticationMessageService, IAuditService auditService) : base(mapper, generalService, noticationMessageService)
        {
            _auditService = auditService;
        }

        [HttpGet("getById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _auditService.ExistById(id) == false)
                return CustomResponse();

            var model = _mapperService.Map<AuditReturnedDto>(await _auditService.GetById(id));

            return CustomResponse(model, Constants.SuccessInGetId);
        }

        [HttpPost("GetAllPaginate")]
        public async Task<IActionResult> GetAllPaginate(AuditFilter filter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var model = await _auditService.GetAllPaginate(filter);

            return CustomResponse(model, Constants.SuccessInGetAllPaginate);
        }
    }
}

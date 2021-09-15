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
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;

        public AuditController(IMapper mapper, IGeneralService generalService, INotificationMessageService noticationMessageService, IAuditService auditService) : base(mapper, generalService, noticationMessageService)
        {
            _auditService = auditService;
        }

        [HttpGet("getById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = _mapperService.Map<AuditReturnedDto>(await _auditService.GetById(id));
            if (record == null)
            {
                return NotFound();
            }

            return CustomResponse(record);
        }

        [HttpPost("GetAllFilter")]
        public async Task<IActionResult> GetAllFilter(AuditFilter filter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _auditService.GetAllWithPaginate(filter));
        }
    }
}

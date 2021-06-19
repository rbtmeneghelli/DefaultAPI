using AutoMapper;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers.Base;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize("Bearer")]
    [AllowAnonymous]
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;

        public AuditController(IMapper mapper, IGeneralService generalService, IAuditService auditService) : base(mapper, generalService)
        {
            _auditService = auditService;
        }

        [HttpGet("v1/getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = _mapper.Map<AuditReturnedDto>(await _auditService.GetById(id));
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPost("v1/GetAllFilter")]
        public async Task<ActionResult<PagedResult<AuditReturnedDto>>> GetAllFilter(AuditFilter filter)
        {
            return Ok(await _auditService.GetAllWithPaginate(filter));
        }
    }
}

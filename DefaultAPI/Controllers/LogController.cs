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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    [ApiExplorerSettings(GroupName = "1.0")]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(IMapper mapper, IGeneralService generalService, ILogService logService) : base(mapper, generalService)
        {
            _logService = logService;
        }

        [HttpGet("getById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = _mapper.Map<LogReturnedDto>(await _logService.GetById(id));
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPost("GetAllFilter")]
        public async Task<ActionResult<PagedResult<LogReturnedDto>>> GetAllFilter(LogFilter filter)
        {
            return Ok(await _logService.GetAllWithPaginate(filter));
        }
    }
}

using DefaultAPI.Application.Queries;
using DefaultAPI.Domain.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultCQRSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegionController : BaseController
    {
        public RegionController(ILogger<RegionController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet("RegionQueryId")]
        public async Task<IActionResult> GetById()
        {
            try
            {
                var response = await _mediator.Send(new RegionQueryById());
                if (response != null)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpGet("RegionQueryAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _mediator.Send(new RegionQueryAll());
                if (response != null)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpPost("RegionQueryPage")]
        public async Task<IActionResult> GetByPage([FromBody] RegionFilter regionFilter)
        {
            var response = await _mediator.Send(new RegionQueryPage(regionFilter));
            if (response != null)
                return Ok(response);

            return BadRequest();
        }
    }
}

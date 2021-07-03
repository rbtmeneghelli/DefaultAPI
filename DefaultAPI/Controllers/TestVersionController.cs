using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultAPI.Controllers
{
    /// <summary>
    /// EXEMPLO DE VERSIONAMENTO DE API
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    // [ApiExplorerSettings(IgnoreApi = true)] // Ignora completamente os endpoints da controller
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class TestVersionController : ControllerBase
    {
        [HttpGet("Someaction")]
        public async Task<IActionResult> SomeAction()
        {
            var list = Enumerable.Range(1, 10);
            return Ok(list);
        }
    }
}


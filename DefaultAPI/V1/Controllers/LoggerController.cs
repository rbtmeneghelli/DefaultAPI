using AutoMapper;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public sealed class LoggerController : BaseController
    {
        private readonly ILoggerService<LoggerController> _logger;

        public LoggerController(ILoggerService<LoggerController> logger, IMapper mapper, IGeneralService generalService, INotificationMessageService notificationMessageService): base(mapper, generalService, notificationMessageService)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.Handle(new LogMessage("testando...", EnumLogger.LogInformation));
            _logger.Handle(new LogMessage("testando...2", EnumLogger.LogTrace));
            _logger.GetLoggerMessages();
            return Ok();
        }
    }
}
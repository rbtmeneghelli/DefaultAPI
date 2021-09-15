using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefaultAPI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DefaultAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapperService;
        protected readonly IGeneralService _generalService;
        protected readonly INotificationMessageService _notificationService;

        protected long UserId { get; set; }
        protected string UserName { get; set; }

        protected BaseController(IMapper mapperService, IGeneralService generalService, INotificationMessageService notificationService)
        {
            _mapperService = mapperService;
            _generalService = generalService;
            _notificationService = notificationService;

            if (generalService.IsAuthenticated())
            {
                UserId = _generalService.GetCurrentUserId();
                UserName = _generalService.GetCurrentUserName();
            }
        }

        protected bool OperationIsValid()
        {
            return !_notificationService.HaveNotification();
        }

        protected IActionResult CustomResponse(object result = null, string message = "")
        {
            if (OperationIsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                    message = message
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificationService.GetNotifications().Select(n => n.Mensagem)
            });
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificationModelIsInvalid(modelState);
            return CustomResponse();
        }

        protected void NotificationModelIsInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificationError(errorMsg);
            }
        }

        protected void NotificationError(string mensagem)
        {
            _notificationService.Handle(new Domain.Models.NotificationMessage(mensagem));
        }
    }
}

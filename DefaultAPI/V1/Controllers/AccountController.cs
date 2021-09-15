using AutoMapper;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain;
using DefaultAPI.Controllers;

namespace DefaultAPI.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;

        public AccountController(IMapper mapper, IGeneralService generalService, INotificationMessageService notificationMessageService, IAccountService accountService) : base(mapper, generalService, notificationMessageService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _accountService.CheckUserAuthentication(loginUser);

            if (result)
            {
                Credentials credentials = await _accountService.GetUserCredentials(loginUser.Login);
                credentials.Token = _generalService.CreateJwtToken(credentials);
                return CustomResponse(credentials);
            }
            else
            {
                return CustomResponse();
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] User user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _accountService.ChangePassword(UserId, user);
            if (result)
                return CustomResponse(null, Constants.SuccessInChangePassword);

            return CustomResponse();
        }

        [HttpGet("ResetPassword/{email:string}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var result = await _accountService.ResetPassword(email);

            if (result)
                return CustomResponse(null, Constants.SuccessInResetPassword);

            return CustomResponse();
        }
    }
}

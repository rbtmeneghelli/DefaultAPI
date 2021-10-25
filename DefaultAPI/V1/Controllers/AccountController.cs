using AutoMapper;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain;
using DefaultAPI.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace DefaultAPI.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public sealed class AccountController : BaseController
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
                return CustomResponse(_generalService.CreateJwtToken(credentials));
            }
            else
            {
                NotificationError("Autenticação invalida. tente novamente!");
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

        [HttpGet("ResetPassword/{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var result = await _accountService.ResetPassword(email);

            if (result)
                return CustomResponse(null, Constants.SuccessInResetPassword);

            return CustomResponse();
        }

        [HttpPost("LoginRefresh")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _accountService.CheckUserAuthentication(loginUser);

            if (result)
            {
                Credentials credentials = await _accountService.GetUserCredentials(loginUser.Login);
                string dataToken = _generalService.CreateJwtToken(credentials);
                var dataRefreshToken = _generalService.GenerateRefreshToken();
                _generalService.SaveRefreshToken(credentials.Login, dataRefreshToken);
                return CustomResponse(new { token = dataToken, refreshToken = dataRefreshToken });
            }
            else
            {
                NotificationError("Autenticação invalida. tente novamente!");
                return CustomResponse();
            }
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] Tokens tokens)
        {
            var principal = _generalService.GetPrincipalFromExpiredToken(tokens.Token);
            var savedRefreshToken = _generalService.GetRefreshToken(principal.Identity.Name);
            if (savedRefreshToken != tokens.RefreshToken)
                throw new SecurityTokenException(Constants.ErrorInRefreshToken);

            var newJwtToken = _generalService.GenerateToken(principal.Claims);
            var newRefreshToken = _generalService.GenerateRefreshToken();
            _generalService.DeleteRefreshToken(principal.Identity.Name, tokens.RefreshToken);
            _generalService.SaveRefreshToken(principal.Identity.Name, newRefreshToken);

            return CustomResponse(new Tokens() { Token = newJwtToken, RefreshToken = newRefreshToken });
        }
    }
}

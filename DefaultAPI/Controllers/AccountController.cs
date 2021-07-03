﻿using AutoMapper;
using DefaultAPI.Controllers.Base;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;

namespace DefaultAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;

        public AccountController(IMapper mapper, IGeneralService generalService, IAccountService accountService) : base(mapper, generalService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login/{login}/{senha}")]
        public async Task<IActionResult> Login(string login, string senha)
        {
            ResultReturned resultReturned = await _accountService.CheckUserAuthentication(login, senha);
            if (resultReturned.Result)
            {
                Credentials credentials = await _accountService.GetUserCredentials(login);
                credentials.Token = _generalService.CreateJwtToken(credentials);
                return Ok(credentials);
            }
            else
            {
                return BadRequest(resultReturned);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] User user)
        {
            ResultReturned resultReturned = await _accountService.ChangePassword(_generalService.GetCurrentUserId(), user);
            if (resultReturned.Result)
                return Ok(resultReturned);
            return BadRequest(resultReturned);
        }

        [HttpGet("ResetPassword/{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            ResultReturned resultReturned = await _accountService.ResetPassword(email);

            if (resultReturned.Result)
                return Ok(resultReturned);
            return BadRequest(resultReturned);
        }
    }
}

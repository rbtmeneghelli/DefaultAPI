using AutoMapper;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers;
using System;
using DefaultAPI.Domain;
using System.Collections.Generic;

namespace DefaultAPI.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    public sealed class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IGeneralService generalService, INotificationMessageService noticationMessageService, IUserService userService) : base(mapper, generalService, noticationMessageService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var model = _mapperService.Map<List<UserReturnedDto>>(await _userService.GetAll());

            return CustomResponse(model, Constants.SuccessInGetAll);
        }

        [HttpPost("GetAllPaginate")]
        public async Task<IActionResult> GetAllPaginate([FromBody] UserFilter userFilter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var model = await _userService.GetAllPaginate(userFilter);

            return CustomResponse(model, Constants.SuccessInGetAllPaginate);
        }

        [HttpGet("GetById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _userService.ExistById(id) == false)
                return CustomResponse();

            var model = _mapperService.Map<UserReturnedDto>(await _userService.GetById(id));

            return CustomResponse(model, Constants.SuccessInGetId);
        }

        [HttpGet("GetByLogin/{login}")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            if (await _userService.ExistByLogin(login) == false)
                return CustomResponse();

            var model = _mapperService.Map<UserReturnedDto>(await _userService.GetByLogin(login));

            return CustomResponse(model, Constants.SuccessInGetId);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return CustomResponse(await _userService.GetUsers(), Constants.SuccessInDdl);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserSendDto userSendDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            User user = _mapperService.Map<User>(userSendDto);

            var result = await _userService.Add(user);

            if (result)
                return CreatedAtAction(nameof(Add), user);

            return CustomResponse();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserSendDto userSendDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            User user = _mapperService.Map<User>(userSendDto);

            if (id != userSendDto.Id)
            {
                NotificationError(Constants.ErrorInGetId);
                return CustomResponse();
            }

            var result = await _userService.Update(id, user);

            if (result)
                return NoContent();

            return CustomResponse();
        }

        [HttpDelete("Delete/{id:long}/{isDeletePhysical:bool}")]
        public async Task<IActionResult> Delete(int id, bool isDeletePhysical = false)
        {
            try
            {
                if (isDeletePhysical)
                {
                    if (await _userService.CanDelete(id))
                    {
                        bool result = await _userService.DeletePhysical(id);
                        if (result)
                            NotificationError(Constants.ErrorInDeletePhysical);
                    }
                }
                else
                {
                    bool result = await _userService.DeleteLogic(id);
                    if (result)
                        NotificationError(Constants.ErrorInDeleteLogic);
                }

                return CustomResponse();
            }
            catch
            {
                if (isDeletePhysical && ProfileId == 1)
                    NotificationError(Constants.ErrorInDeletePhysical);
                else if (isDeletePhysical && ProfileId != 1)
                    NotificationError(Constants.NoAuthorization);
                else
                    NotificationError(Constants.ErrorInDeleteLogic);

                return CustomResponse();
            }
        }

        [HttpPost("Export2Excel")]
        public async Task<IActionResult> Export2Excel([FromBody] UserFilter filter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var list = await _userService.GetAllPaginate(filter);
            var result = await _generalService.Export2Excel(list.Results, "Users");
            return File(result.Memory, result.FileExtension, result.FileName);
        }
    }
}

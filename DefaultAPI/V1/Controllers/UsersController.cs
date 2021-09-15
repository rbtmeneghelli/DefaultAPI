using AutoMapper;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers;

namespace DefaultAPI.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IGeneralService generalService, INotificationMessageService noticationMessageService, IUserService userService) : base(mapper, generalService, noticationMessageService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CustomResponse(await _userService.GetAll());
        }

        [HttpPost("GetAllPaginate")]
        public async Task<IActionResult> GetAllPaginate([FromBody] UserFilter userFilter)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _userService.GetAllPaginate(userFilter));
        }

        [HttpGet("GetById/{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            return CustomResponse(await _userService.GetById(id));
        }

        [HttpGet("GetByLogin/{login}")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            return CustomResponse(await _userService.GetByLogin(login));
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return CustomResponse(await _userService.GetUsers());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserSendDto userSendDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            User user = _mapperService.Map<User>(userSendDto);

            if (ModelState.IsValid)
            {
                var result = await _userService.Add(user);
                if (result)
                    return CreatedAtAction(nameof(Add), result);
            }

            return CustomResponse();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserSendDto userSendDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            User user = _mapperService.Map<User>(userSendDto);

            if (id != userSendDto.Id)
            {
                NotificationError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.Update(id, user);
                if (result)
                    return NoContent();
            }

            return CustomResponse();
        }

        [HttpDelete("Delete/{id:long}/{isDeletePhysical:bool}")]
        public async Task<IActionResult> Delete(int id, bool isDeletePhysical = false)
        {
            var result = await _userService.Delete(id, isDeletePhysical);
            if (result)
                return CustomResponse();

            return CustomResponse();
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

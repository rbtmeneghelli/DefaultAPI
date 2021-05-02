using AutoMapper;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;

namespace DefaultAPI.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IGeneralService generalService, IUserService userService) : base(mapper, generalService)
        {
            _userService = userService;
        }

        [HttpGet("v1/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Task.FromResult(_userService.GetAll()));
        }

        [HttpPost("v1/GetAllPaginate")]
        public async Task<IActionResult> GetAllPaginate([FromBody] UserFilter userFilter)
        {
            return Ok(await Task.FromResult(_userService.GetAllPaginate(userFilter)));
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await Task.FromResult(_userService.GetById(id)));
        }

        [HttpGet("v1/GetByLogin/{login}")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            return Ok(await Task.FromResult(_userService.GetByLogin(login)));
        }

        [HttpGet("v1/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Task.FromResult(_userService.GetUsers()));
        }

        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add([FromBody] UserSendDto userSendDto)
        {
            User user = _mapper.Map<User>(userSendDto);
            ResultReturned result = new ResultReturned();

            if (ModelState.IsValid)
            {
                result = await _userService.Add(user);
                if (result.Result)
                    return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("v1/Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserSendDto userSendDto)
        {
            User user = _mapper.Map<User>(userSendDto);
            ResultReturned result = new ResultReturned();

            if (ModelState.IsValid)
            {
                result = await _userService.Update(id, user);
                if (result.Result)
                    return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("v1/Delete/{id}/{isDeletePhysical}")]
        public async Task<IActionResult> Delete(int id, bool isDeletePhysical)
        {
            ResultReturned result = new ResultReturned();
            result = await _userService.Delete(id, isDeletePhysical);
            if (result.Result)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/Export2Excel")]
        public async Task<IActionResult> Export2Excel([FromBody] UserFilter filter)
        {
            var list = await _userService.GetAllPaginate(filter);
            var result = await _generalService.Export2Excel(list.UserReturnedSet, "Users");
            return File(result.Memory, result.FileExtension, result.FileName);
        }
    }
}

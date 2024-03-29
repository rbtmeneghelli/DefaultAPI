﻿using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using ServiceTria.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<UserReturnedDto>> GetAll();
        Task<PagedResult<UserReturnedDto>> GetAllPaginate(UserFilter filter);
        Task<UserReturnedDto> GetById(long id);
        Task<UserReturnedDto> GetByLogin(string login);
        Task<List<DropDownList>> GetUsers();
        Task<bool> Add(User user);
        Task<bool> Update(long id, User user);
        Task<bool> DeletePhysical(long id);
        Task<bool> DeleteLogic(long id);
        Task<bool> ExistById(long id);
        Task<bool> CanDelete(long id);
        Task<bool> ReactiveUser(long id);
        Task<bool> ExistByLogin(string login);
    }
}

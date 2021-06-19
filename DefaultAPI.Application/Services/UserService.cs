using DefaultAPI.Domain;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.CrossCutting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Application.Factory;

namespace DefaultAPI.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserReturnedDto>> GetAll()
        {
            return await (from p in _userRepository.GetAll().Include(x => x.Profile)
                          orderby p.Login ascending
                          select new UserReturnedDto()
                          {
                              Id = p.Id,
                              Login = p.Login,
                              IsAuthenticated = p.IsAuthenticated,
                              IsActive = p.IsActive,
                              Password = "-",
                              LastPassword = "-",
                              Profile = p.Profile.ProfileType.GetDisplayName(),
                              IdProfile = p.Profile.Id.Value,
                              Status = p.IsActive ? "Ativo" : "Inativo"
                          }).ToListAsync();
        }

        public async Task<PagedResult<UserReturnedDto>> GetAllPaginate(UserFilter filter)
        {
            var query = GetAllUsers(filter);

            var queryResult = from p in query.AsQueryable()
                              orderby p.Login ascending
                              select new UserReturnedDto
                              {
                                  Id = p.Id,
                                  Login = p.Login,
                                  IsAuthenticated = p.IsAuthenticated,
                                  IsActive = p.IsActive,
                                  Password = "-",
                                  LastPassword = "-",
                                  Profile = p.Profile.ProfileType.GetDisplayName(),
                                  Status = p.IsActive ? "Ativo" : "Inativo"
                              };

            return PagedFactory.GetPaged(queryResult, filter.pageIndex, filter.pageSize);
        }

        public async Task<UserReturnedDto> GetById(long id)
        {
            return await (from p in _userRepository.FindBy(x => x.Id == id).AsQueryable()
                          orderby p.Login ascending
                          select new UserReturnedDto
                          {
                              Id = p.Id,
                              Login = p.Login,
                              IsAuthenticated = p.IsAuthenticated,
                              IsActive = p.IsActive,
                              Password = "-",
                              LastPassword = "-",
                              Status = p.IsActive ? "Ativo" : "Inativo"
                          }).FirstOrDefaultAsync();
        }

        public async Task<UserReturnedDto> GetByLogin(string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                return await (from p in _userRepository.FindBy(x => x.Login.ToUpper().Trim() == login.ToUpper().Trim()).AsQueryable()
                              orderby p.Login ascending
                              select new UserReturnedDto
                              {
                                  Id = p.Id,
                                  Login = p.Login,
                                  IsAuthenticated = p.IsAuthenticated,
                                  IsActive = p.IsActive,
                                  Password = "-",
                                  LastPassword = "-",
                                  Profile = p.Profile.ProfileType.GetDisplayName(),
                                  Status = p.IsActive ? "Ativo" : "Inativo"
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<DropDownList>> GetUsers()
        {
            return await _userRepository.FindBy(x => x.IsActive == true && x.IsAuthenticated == true)
                   .Select(x => new DropDownList()
                   {
                       Id = x.Id.Value,
                       Description = x.Login
                   }).ToListAsync();
        }

        public bool ExistById(int id)
        {
            return _userRepository.Exist(x => x.IsActive == true && x.Id == id);
        }

        public bool ExistByLogin(string Login)
        {
            return _userRepository.Exist(x => x.IsActive == true && x.Login == Login.ToUpper().Trim());
        }

        public async Task<ResultReturned> Add(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Login) && string.IsNullOrWhiteSpace(user.Password))
                    return new ResultReturned() { Result = false, Message = "Para realizar o login, o campo login e senha deve ser preenchido" };

                else if (!ExistByLogin(user.Login))
                {
                    user.Password = new HashingManager().HashToString(user.Password);
                    user.CreatedTime = DateTime.Now;
                    _userRepository.Add(user);
                    _userRepository.SaveChanges();
                    return new ResultReturned() { Result = true, Message = Constants.SuccessInAdd };
                }

                return new ResultReturned() { Result = false, Message = $"Já existe um login {user.Login} cadastrado, troque de login" };
            }
            catch (Exception)
            {
                return new ResultReturned() { Result = false, Message = Constants.ErrorInAdd };
            }
        }

        public async Task<ResultReturned> Update(long id, User user)
        {
            User userDb = _userRepository.GetById(id);

            if (userDb is not null)
            {
                userDb.LastPassword = userDb.Password;
                userDb.Password = new HashingManager().HashToString(user.Password);
                userDb.UpdateTime = DateTime.Now;
                _userRepository.Update(userDb);
                _userRepository.SaveChanges();
                return new ResultReturned() { Result = true, Message = Constants.SuccessInUpdate };
            }

            return new ResultReturned() { Result = true, Message = Constants.ErrorInUpdate };
        }

        public async Task<ResultReturned> Delete(long id, bool isDeletePhysical = false)
        {
            User user = _userRepository.GetById(id);

            if (isDeletePhysical && user is not null)
                _userRepository.Remove(user);

            else if (!isDeletePhysical && user is not null)
            {
                user.UpdateTime = DateTime.Now;
                user.IsActive = user.IsActive ? false : true;
                _userRepository.Update(user);
                _userRepository.SaveChanges();
                return new ResultReturned() { Result = true, Message = Constants.SuccessInDelete };
            }

            return new ResultReturned() { Result = false, Message = Constants.ErrorInDelete };
        }

        private IQueryable<User> GetAllUsers(UserFilter filter)
        {
            return _userRepository.GetAllTracking().Include(x => x.Profile).Where(GetPredicate(filter)).AsQueryable();
        }

        private Expression<Func<User, bool>> GetPredicate(UserFilter filter)
        {
            return p =>
                   (string.IsNullOrWhiteSpace(filter.Login) || p.Login.Trim().ToUpper().Contains(filter.Login.Trim().ToUpper()))
                   &&
                   (!filter.IsAuthenticated.HasValue || filter.IsAuthenticated == p.IsAuthenticated)
                   &&
                   (!filter.IsActive.HasValue || filter.IsActive == p.IsActive)
                   &&
                   (!filter.IdProfile.HasValue || filter.IdProfile == p.IdProfile);
        }
    }
}

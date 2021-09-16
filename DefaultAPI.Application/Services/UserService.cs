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
    public class UserService : BaseService, IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, INotificationMessageService notificationMessageService) : base(notificationMessageService)
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
            try
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                await Task.CompletedTask;
            }
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

        public async Task<bool> Add(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Login) && string.IsNullOrWhiteSpace(user.Password))
                {
                    Notify("Para realizar o login, o campo login e senha deve ser preenchido");
                    return false;
                }

                else if (!ExistByLogin(user.Login))
                {
                    user.Password = new HashingManager().HashToString(user.Password);
                    user.CreatedTime = DateTime.Now;
                    _userRepository.Add(user);
                    _userRepository.SaveChanges();
                    return true;
                }

                Notify($"Já existe um login {user.Login} cadastrado, troque de login");
                return false;
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInAdd);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> Update(long id, User user)
        {
            try
            {
                User userDb = _userRepository.GetById(id);

                if (userDb is not null)
                {
                    userDb.LastPassword = userDb.Password;
                    userDb.Password = new HashingManager().HashToString(user.Password);
                    userDb.UpdateTime = DateTime.Now;
                    _userRepository.Update(userDb);
                    _userRepository.SaveChanges();
                    return true;
                }
                Notify(Constants.ErrorInUpdate);
                return false;
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInUpdate);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> Delete(long id, bool isDeletePhysical = false)
        {
            try
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
                    return true;
                }

                return true;
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInDelete);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        private IQueryable<User> GetAllUsers(UserFilter filter)
        {
            // Exemplo Utilizando SimpleFactory
            // var obj = UserFactory.GetData(EnumProfileType.Admin);
            // return _userRepository.GetAllTracking().Include(x => x.Profile).Where(obj.GetPredicate(filter)).AsQueryable();
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

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}

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
            try
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
            catch
            {
                Notify(Constants.ErrorInGetAll);
                return new List<UserReturnedDto>();
            }
            finally
            {
                await Task.CompletedTask;
            }
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
            catch (Exception ex)
            {
                Notify(Constants.ErrorInGetAll);
                return null;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<UserReturnedDto> GetById(long id)
        {
            try
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
            catch
            {
                Notify(Constants.ErrorInGetId);
                return new UserReturnedDto();
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<UserReturnedDto> GetByLogin(string login)
        {
            try
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
            catch
            {
                Notify("Ocorreu um erro ao efetuar a pesquisa a partir do login. Entre em contato com o administrador");
                return new UserReturnedDto();
            }
            finally
            {
                await Task.CompletedTask;
            }

            return null;
        }

        public async Task<List<DropDownList>> GetUsers()
        {
            try
            {
                return await _userRepository.FindBy(x => x.IsActive == true && x.IsAuthenticated == true)
                             .Select(x => new DropDownList()
                             {
                                 Id = x.Id.Value,
                                 Description = x.Login
                             }).ToListAsync();
            }
            catch
            {
                Notify(Constants.ErrorInGetDdl);
                return new List<DropDownList>();
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> ExistById(long id)
        {
            try
            {
                var result = _userRepository.Exist(x => x.IsActive == true && x.Id == id);

                if (result == false)
                    Notify(Constants.ErrorInGetId);

                return result;
            }
            catch
            {
                Notify(Constants.ErrorInGetId);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> ExistByLogin(string login)
        {
            try
            {
                if (string.IsNullOrEmpty(login)) { 
                    Notify("O campo login está em branco, por favor preencha!");
                    return false;
                }

                var result = _userRepository.Exist(x => x.IsActive == true && x.Login == login.ToUpper().Trim());

                if (result == false)
                    Notify("Ocorreu um erro para pesquisar o registro do login solicitado. Entre em contato com o Administrador");

                return result;
            }
            catch
            {
                Notify("Ocorreu um erro para pesquisar o registro do login solicitado. Entre em contato com o Administrador");
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
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

                else if (_userRepository.Exist(x => x.IsActive == true && x.Login == user.Login.ToUpper().Trim()) == false)
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

        public async Task<bool> DeletePhysical(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);

                if (user is not null)
                {
                    _userRepository.Remove(user);
                    return true;
                }
                else
                {
                    Notify(Constants.ErrorInDeletePhysical);
                    return false;
                }
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInDeletePhysical);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> DeleteLogic(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);

                if (user is not null)
                {
                    user.UpdateTime = DateTime.Now;
                    user.IsActive = false;
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return true;
                }
                else
                {
                    Notify(Constants.ErrorInDeleteLogic);
                    return false;
                }
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInDeleteLogic);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> CanDelete(long id)
        {
            try
            {
                return await _userRepository.CanDelete(id);
            }
            catch
            {
                Notify(Constants.ErrorInDeletePhysical);
                return false;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        public async Task<bool> ReactiveUser(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);

                if (user is not null)
                {
                    user.UpdateTime = DateTime.Now;
                    user.IsActive = true;
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                Notify(Constants.ErrorInActiveRecord);
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
                   (string.IsNullOrWhiteSpace(filter.Login) || p.Login.Trim().ToUpper().StartsWith(filter.Login.Trim().ToUpper()))
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

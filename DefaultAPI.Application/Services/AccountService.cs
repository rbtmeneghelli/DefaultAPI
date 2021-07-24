using DefaultAPI.Domain;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.CrossCutting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Enums;

namespace DefaultAPI.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IGeneralService _generalService;

        public AccountService(IRepository<User> userRepository, IGeneralService generalService)
        {
            _userRepository = userRepository;
            _generalService = generalService;
        }

        public async Task<ResultReturned> CheckUserAuthentication(string login, string password)
        {
            try
            {
                User user = await _userRepository.FindBy(x => x.Login.ToUpper().Trim() == login.ToUpper().Trim() && x.IsActive == true).FirstOrDefaultAsync();
                if (user is not null)
                {
                    if (new HashingManager().Verify(password, user.Password))
                        return new ResultReturned() { Result = true, Message = "Usuário OK" };

                    return new ResultReturned() { Result = false, Message = "Autenticação invalida" };
                }

                return new ResultReturned() { Result = false, Message = "Erro na validação" };
            }
            catch (Exception)
            {
                return new ResultReturned() { Result = false, Message = "Erro na validação" };
            }
        }

        public async Task<Credentials> GetUserCredentials(string login)
        {
            Credentials credenciais = new Credentials();
            User user = await _userRepository.GetAll().Include("Profile.ProfileOperations.Operation.Roles").Where(p => p.Login == login).FirstOrDefaultAsync();

            if (user != null)
            {
                credenciais.Id = user.Id;
                credenciais.Login = user.Login;
                credenciais.Perfil = user.Profile.Description;
                credenciais.Roles = new List<string>() { };
                foreach (var item in user.Profile.ProfileOperations)
                {
                    List<EnumActions> condition = GetActions(item);
                    credenciais.Roles.AddRange(item.Operation.Roles.Where(y => condition.Contains(y.Action)).Select(z => z.RoleTag).ToList());
                }
            }

            return credenciais;
        }

        public async Task<ResultReturned> ChangePassword(long id, User user)
        {
            User dbUser = _userRepository.GetById(id);
            if (dbUser != null)
            {
                if (new HashingManager().Verify(user.Password, dbUser.Password) && dbUser.Login.ToUpper() == user.Login.ToUpper())
                {
                    dbUser.LastPassword = dbUser.Password;
                    dbUser.Password = new HashingManager().HashToString(user.Password);
                    dbUser.IsAuthenticated = true;
                    dbUser.UpdateTime = DateTime.Now;
                    _userRepository.Update(dbUser);
                    _userRepository.SaveChanges();
                    return new ResultReturned() { Result = true, Message = Constants.SuccessInChangePassword };
                }
            }
            return new ResultReturned() { Result = false, Message = Constants.ErrorInChangePassword };
        }

        public async Task<ResultReturned> ResetPassword(string login)
        {
            string password = "123mudar";

            if (!string.IsNullOrWhiteSpace(login))
            {
                User user = await _userRepository.FindBy(x => x.Login == login.ToUpper().Trim()).FirstOrDefaultAsync();
                if (user is not null)
                {
                    user.LastPassword = user.Password;
                    user.Password = new HashingManager().HashToString("123mudar");
                    user.IsAuthenticated = false;
                    user.IsActive = true;
                    user.CreatedTime = DateTime.Now;
                    _generalService.SendEmail(new EmailConfig("Reset de senha", "roberto.mng.89@gmail.com", "Reset de Senha - Admin", $"Caro Administrador, <br> Sua senha de administrador foi resetada com sucesso. <br> Segue a sua nova senha: {password} ", "Roberto")).Wait();
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return new ResultReturned() { Result = true, Message = Constants.SuccessInResetPassword };
                }

                return new ResultReturned() { Result = false, Message = Constants.ErrorInResetPassword };
            }
            return new ResultReturned() { Result = false, Message = Constants.ErrorInResetPassword };
        }

        private List<EnumActions> GetActions(ProfileOperation profileOperation)
        {
            List<EnumActions> condition = new List<EnumActions>();
            condition.Add(profileOperation.CanCreate ? EnumActions.Insert : EnumActions.None);
            condition.Add(profileOperation.CanResearch ? EnumActions.Research : EnumActions.None);
            condition.Add(profileOperation.CanUpdate ? EnumActions.Update : EnumActions.None);
            condition.Add(profileOperation.CanDelete ? EnumActions.Delete : EnumActions.None);
            condition.Add(profileOperation.CanExport ? EnumActions.Export : EnumActions.None);
            condition.Add(profileOperation.CanImport ? EnumActions.Import : EnumActions.None);
            return condition;
        }
    }
}


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
    public class AccountService : BaseService, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGeneralService _generalService;

        public AccountService(IUserRepository userRepository, IGeneralService generalService, INotificationMessageService notificationMessageService) : base(notificationMessageService)
        {
            _userRepository = userRepository;
            _generalService = generalService;
        }

        public async Task<bool> CheckUserAuthentication(LoginUser loginUser)
        {
            try
            {
                User user = await _userRepository.FindBy(x => x.Login.ToUpper().Trim() == loginUser.Login.ToUpper().Trim() && x.IsActive == true).FirstOrDefaultAsync();
                if (user is not null)
                {
                    if (new HashingManager().Verify(loginUser.Password, user.Password))
                        return true;

                    Notify("Autenticação invalida");
                }

                Notify("Erro na validação");
                return false;
            }
            catch (Exception)
            {
                Notify("Erro na validação");
                return false;
            }
        }

        public async Task<Credentials> GetUserCredentials(string login)
        {
            Credentials credenciais = new Credentials();
            User user = await _userRepository.GetUserCredentialsByLogin(login);

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

        public async Task<bool> ChangePassword(long id, User user)
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
                    await Task.CompletedTask;
                    return true;
                }
            }

            Notify(Constants.ErrorInChangePassword);
            await Task.CompletedTask;
            return false;
        }

        public async Task<bool> ResetPassword(string login)
        {
            if (!string.IsNullOrWhiteSpace(login))
            {
                User user = await _userRepository.FindBy(x => x.Login == login.ToUpper().Trim()).FirstOrDefaultAsync();
                if (user is not null)
                {
                    user.LastPassword = user.Password;
                    user.Password = new HashingManager().HashToString(Constants.DefaultPassword);
                    user.IsAuthenticated = false;
                    user.IsActive = true;
                    user.CreatedTime = DateTime.Now;
                    _generalService.SendEmail(new EmailConfig("Reset de senha", "roberto.mng.89@gmail.com", "Reset de Senha - Admin", $"Caro Administrador, <br> Sua senha de administrador foi resetada com sucesso. <br> Segue a sua nova senha: {Constants.DefaultPassword} ", "Roberto")).Wait();
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return true;
                }

                Notify(Constants.ErrorInResetPassword);
            }

            Notify(Constants.ErrorInResetPassword);
            return false;
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

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}


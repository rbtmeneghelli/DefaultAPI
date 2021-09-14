using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<bool> CheckUserAuthentication(LoginUser loginUser);
        Task<Credentials> GetUserCredentials(string login);
        Task<bool> ChangePassword(long id, User user);
        Task<bool> ResetPassword(string email);
    }
}

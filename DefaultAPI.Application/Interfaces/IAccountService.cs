using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResultReturned> CheckUserAuthentication(string login, string password);
        Task<Credentials> GetUserCredentials(string login);
        Task<ResultReturned> ChangePassword(long id, User user);
        Task<ResultReturned> ResetPassword(string email);
    }
}

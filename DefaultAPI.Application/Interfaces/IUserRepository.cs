using DefaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserCredentialsByLogin(string login);
        Task<User> GetUserCredentialsById(long id);
        Task<bool> CanDelete(long userId);
    }
}


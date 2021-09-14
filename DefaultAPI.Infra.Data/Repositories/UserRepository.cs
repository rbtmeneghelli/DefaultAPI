using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DefaultAPIContext context) : base(context) { }

        public async Task<User> GetUserCredentialsById(long id)
        {
            return await _context.User.Include("Profile.ProfileOperations.Operation.Roles").Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserCredentialsByLogin(string login)
        {
            return await _context.User.Include("Profile.ProfileOperations.Operation.Roles").Where(p => p.Login.ToUpper() == login.ToUpper()).FirstOrDefaultAsync(); ;
        }
    }
}

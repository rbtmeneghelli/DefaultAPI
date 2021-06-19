using DefaultAPI.Application.FactoryInterfaces;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DefaultAPI.Application.Others
{
    public class UserManagerService : IUserFactory
    {
        public override Expression<Func<User, bool>> GetPredicate(UserFilter filter)
        {
            return p => p.IdProfile == 1 && 
                        p.IsActive == true;
        }
    }
}

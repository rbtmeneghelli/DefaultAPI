using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.FactoryInterfaces
{
    public abstract class IUserFactory
    {
        public abstract Expression<Func<User, bool>> GetPredicate(UserFilter filter);
    }
}

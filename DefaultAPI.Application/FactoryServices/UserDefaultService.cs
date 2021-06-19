using DefaultAPI.Application.Factory;
using DefaultAPI.Application.FactoryInterfaces;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Others
{
    public class UserDefaultService : IUserFactory
    {
        public override Expression<Func<User, bool>> GetPredicate(UserFilter filter)
        {
            return p => filter.IdProfile == 2 &&
                        p.IsActive == true;
        }
    }
}

using DefaultAPI.Application.FactoryInterfaces;
using DefaultAPI.Application.Others;
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Application.Factory
{
    public sealed class UserFactory
    {
        public static IUserFactory GetData(EnumProfileType enumProfileType)
        {

            IUserFactory userData;

            switch (enumProfileType)
            {
                case EnumProfileType.User:
                    userData = new UserDefaultService();
                    break;
                case EnumProfileType.Admin:
                    userData = new UserDefaultService();
                    break;
                case EnumProfileType.Manager:
                    userData = new UserDefaultService();
                    break;
                default:
                    throw new ApplicationException();
            }

            return userData;
        }

    }
}

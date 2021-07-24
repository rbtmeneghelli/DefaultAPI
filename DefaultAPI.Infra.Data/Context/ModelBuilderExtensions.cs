using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Infra.CrossCutting;
using DefaultAPI.Infra.Data.Context;
using DefaultAPI.Infra.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultAPI.Infra.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ExecuteSeed(this ModelBuilder modelBuilder)
        {
            SeedProfile(modelBuilder);
            SeedOperation(modelBuilder);
            SeedRole(modelBuilder);
            SeedProfileOperation(modelBuilder);
            SeedUser(modelBuilder);
        }

        private static void SeedUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Login = "admin@DefaultAPI.com.br",
                CreatedTime = DateTime.Now,
                IsActive = true,
                IsAuthenticated = true,
                Password = new HashingManager().HashToString("123mudar"),
                LastPassword = string.Empty,
                IdProfile = 1
            });
        }

        private static void SeedProfile(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().HasData(
               new Profile() { Id = 1, Description = "Perfil Usuário Tria", AccessGroup = EnumAccessGroup.Tria, LoginType = EnumLoginType.Cloud, CreatedTime = DateTime.Now, IsActive = true, ProfileType = EnumProfileType.User },
               new Profile() { Id = 2, Description = "Perfil Administrador Tria", AccessGroup = EnumAccessGroup.Tria, LoginType = EnumLoginType.Cloud, CreatedTime = DateTime.Now, IsActive = true, ProfileType = EnumProfileType.Admin },
               new Profile() { Id = 3, Description = "Perfil Manager Tria", AccessGroup = EnumAccessGroup.Tria, LoginType = EnumLoginType.Cloud, CreatedTime = DateTime.Now, IsActive = true, ProfileType = EnumProfileType.Manager }
            );
        }

        private static void SeedOperation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>().HasData(
              new Operation() { Id = 1, Description = "Auditoria", CreatedTime = DateTime.Now, IsActive = true }
            );
        }

        private static void SeedRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
               new Role() { Id = 1, Description = "Regra de acesso a tela de Auditoria", CreatedTime = DateTime.Now, RoleTag = "ROLE_AUDIT" }
            );
        }

        private static void SeedProfileOperation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileOperation>().HasData(
               new ProfileOperation() { IdProfile = 1, IdOperation = 1 }
            );
        }
    }
}
using DefaultAPI.Domain.Entities;
using DefaultAPI.Infra.Data.Mapping;
using DefaultAPI.Infra.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Context
{
    public class DefaultAPIContext : DbContext
    {
        public DefaultAPIContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(DefaultAPIContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ProfileMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new ProfileRoleMapping());
            modelBuilder.ApplyConfiguration(new StatesMapping());
            modelBuilder.ApplyConfiguration(new CepsMapping());
            modelBuilder.ApplyConfiguration(new RegionMapping());
            modelBuilder.ExecuteSeed();
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileRole> ProfileRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<States> State { get; set; }
        public virtual DbSet<Ceps> Cep { get; set; }
        public virtual DbSet<City> City { get; set; }
    }
}
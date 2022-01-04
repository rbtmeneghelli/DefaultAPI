using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Enums;
using DefaultAPI.Infra.Data.Mapping;
using DefaultAPI.Infra.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Infra.Data.Context
{
    public class DefaultAPIContext : DbContext
    {
        public DefaultAPIContext(DbContextOptions<DefaultAPIContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultAPIContext).Assembly);
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ProfileMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new ProfileOperationMapping());
            modelBuilder.ApplyConfiguration(new StatesMapping());
            modelBuilder.ApplyConfiguration(new CepsMapping());
            modelBuilder.ApplyConfiguration(new RegionMapping());
            modelBuilder.ApplyConfiguration(new AuditMapping());
            modelBuilder.ApplyConfiguration(new LogMapping());
            modelBuilder.ApplyConfiguration(new OperationMapping());
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            // Com o comando abaixo podemos fazer o EF Core carregar apenas os dados de cada uma das entidades que estejam com o campo Ativo == null
            //builder.ApplyGlobalFilters<Base>(x => x.Ativo == null);
            modelBuilder.ExecuteSeed();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedTime") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedTime").CurrentValue = DateTime.Now;
                    entry.Property("UpdateTime").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedTime").IsModified = false;
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
            }

            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.ActionName = EnumActions.Insert.GetDisplayName();
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.ActionName = EnumActions.Delete.GetDisplayName();
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.ActionName = EnumActions.Update.GetDisplayName();
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audit.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                Audit.Add(auditEntry.ToAudit());
            }

            return Task.FromResult(SaveChanges());
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileOperation> ProfileOperations { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<States> State { get; set; }
        public virtual DbSet<Ceps> Cep { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
    }
}
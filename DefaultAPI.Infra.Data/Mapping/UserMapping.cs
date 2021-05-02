using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        private EntityTypeBuilder<User> _builder;

        public void Configure(EntityTypeBuilder<User> builder)
        {
            _builder = builder;
            _builder.ToTable("Users");
            ConfigureColumns();
            ConfigureForeignKeys();
            ConfigureIndexes();
        }

        private void ConfigureForeignKeys()
        {
            _builder.HasOne(x => x.Profile).WithMany(x => x.Users).HasForeignKey(x => x.IdProfile);
        }

        private void ConfigureIndexes()
        {
            _builder.HasIndex(a => a.IdProfile).IsUnique(false);
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Login).IsRequired(true).HasMaxLength(120).HasColumnName("Login");
            _builder.Property(x => x.Password).IsRequired(true).HasMaxLength(255).HasColumnName("Password");
            _builder.Property(x => x.LastPassword).IsRequired(false).HasMaxLength(255).HasColumnName("Last_Password");
            _builder.Property(x => x.IsAuthenticated).IsRequired(true).HasDefaultValue(false).HasColumnName("Is_Authenticated");
            _builder.Property(x => x.CreatedTime).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).HasColumnName("Is_Active");
        }
    }
}

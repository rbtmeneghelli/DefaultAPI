using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        private EntityTypeBuilder<Role> _builder;

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            _builder = builder;
            _builder.ToTable("Roles");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Description).IsRequired(true).HasMaxLength(255).HasColumnName("Description");
            _builder.Property(x => x.RoleTag).IsRequired(true).HasMaxLength(80).HasColumnName("Role");
            _builder.Property(x => x.CreatedTime).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).HasDefaultValue(true).HasColumnName("Is_Active");
            _builder.HasOne(x => x.Operation).WithMany(x => x.Roles).HasForeignKey(x => x.IdOperation).HasConstraintName("IdOperation");
        }
    }
}

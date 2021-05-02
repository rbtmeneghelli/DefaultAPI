using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class AuditMapping : IEntityTypeConfiguration<Audit>
    {
        private EntityTypeBuilder<Audit> _builder;

        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            _builder = builder;
            _builder.ToTable("Audits");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.TableName).IsRequired().HasMaxLength(100).HasColumnName("Table_Name");
            _builder.Property(x => x.ActionName).IsRequired().HasMaxLength(80).HasColumnName("Action_Name");
            _builder.Property(x => x.KeyValues).IsRequired(false).HasMaxLength(10000).HasColumnName("Key_Values");
            _builder.Property(x => x.OldValues).IsRequired(false).HasMaxLength(10000).HasColumnName("Old_Values");
            _builder.Property(x => x.NewValues).IsRequired(false).HasMaxLength(10000).HasColumnName("New_Values");
            _builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("Is_Active");
        }
    }
}

using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        private EntityTypeBuilder<Log> _builder;

        public void Configure(EntityTypeBuilder<Log> builder)
        {
            _builder = builder;
            _builder.ToTable("Logs");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Class).IsRequired().HasMaxLength(100).HasColumnName("Class");
            _builder.Property(x => x.Method).IsRequired().HasMaxLength(100).HasColumnName("Method");
            _builder.Property(x => x.MessageError).IsRequired(false).HasMaxLength(10000).HasColumnName("Message_Error");
            _builder.Property(x => x.Object).IsRequired(false).HasMaxLength(10000).HasColumnName("Object");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
        }
    }
}

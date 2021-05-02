using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class RegionMapping : IEntityTypeConfiguration<Region>
    {
        private EntityTypeBuilder<Region> _builder;

        public void Configure(EntityTypeBuilder<Region> builder)
        {
            _builder = builder;
            _builder.ToTable("Regions");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Nome).IsRequired().HasMaxLength(100).HasColumnName("Name");
            _builder.Property(x => x.Sigla).IsRequired().HasMaxLength(5).HasColumnName("Initials");
            _builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("Is_Active");
        }
    }
}
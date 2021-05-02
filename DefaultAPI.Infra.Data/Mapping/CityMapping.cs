using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        private EntityTypeBuilder<City> _builder;

        public void Configure(EntityTypeBuilder<City> builder)
        {
            _builder = builder;
            _builder.ToTable("Cities");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.IBGE).IsRequired().HasMaxLength(255).HasColumnName("Ibge");
            _builder.Property(x => x.Name).HasMaxLength(255).HasColumnName("City");
            _builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("Is_Active");
            _builder.HasOne(x => x.States).WithMany(x => x.City).HasForeignKey(x => x.IdState);
        }
    }
}

using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class CepsMapping : IEntityTypeConfiguration<Ceps>
    {
        private EntityTypeBuilder<Ceps> _builder;

        public void Configure(EntityTypeBuilder<Ceps> builder)
        {
            _builder = builder;
            _builder.ToTable("Ceps");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Cep).IsRequired().HasMaxLength(255).HasColumnName("Cep");
            _builder.Property(x => x.Logradouro).HasMaxLength(255).HasColumnName("Address");
            _builder.Property(x => x.Complemento).HasMaxLength(255).HasColumnName("Complement");
            _builder.Property(x => x.Bairro).HasMaxLength(255).HasColumnName("District");
            _builder.Property(x => x.Uf).IsRequired().HasMaxLength(2).HasColumnName("Uf");
            _builder.Property(x => x.Ibge).HasMaxLength(255).HasColumnName("Ibge");
            _builder.Property(x => x.Gia).HasMaxLength(255).HasColumnName("Gia");
            _builder.Property(x => x.Ddd).HasMaxLength(255).HasColumnName("Ddd");
            _builder.Property(x => x.Siafi).HasMaxLength(255).HasColumnName("Siafi");
            _builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("Is_Active");
            _builder.HasOne(x => x.Estado).WithMany(x => x.Ceps).HasForeignKey(x => x.IdEstado);
        }
    }
}
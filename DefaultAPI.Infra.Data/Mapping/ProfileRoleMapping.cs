using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultAPI.Infra.Data.Mapping
{

    public class ProfileRoleMapping : IEntityTypeConfiguration<ProfileRole>
    {
        private EntityTypeBuilder<ProfileRole> _builder;

        public void Configure(EntityTypeBuilder<ProfileRole> builder)
        {
            _builder = builder;
            _builder.ToTable("ProfileRoles");
            ConfigurePrimaryKey();
            ConfigureColumns();
            ConfigureForeignKeys();
            ConfigureIndexes();
        }

        private void ConfigureColumns()
        {
            _builder.Property(a => a.IdProfile).IsRequired(true).HasColumnName("Id_Profile");
            _builder.Property(a => a.IdRole).IsRequired(true).HasColumnName("Id_Role");
        }

        private void ConfigureForeignKeys()
        {
            _builder.HasOne(a => a.Profile)
                .WithMany(a => a.ProfileRoles)
                .HasForeignKey(a => a.IdProfile);

            _builder.HasOne(a => a.Role)
                .WithMany(a => a.ProfileRoles)
                .HasForeignKey(a => a.IdRole);
        }

        private void ConfigureIndexes()
        {
            _builder.HasIndex(a =>
                new
                {
                    a.IdProfile,
                    a.IdRole
                })
                .IsUnique(true);
        }

        private void ConfigurePrimaryKey()
        {
            _builder.HasKey(a => new
            {
                a.IdProfile,
                a.IdRole
            });
        }
    }
}
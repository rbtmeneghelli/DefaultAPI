using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultAPI.Infra.Data.Mapping
{

    public class ProfileOperationMapping : IEntityTypeConfiguration<ProfileOperation>
    {
        private EntityTypeBuilder<ProfileOperation> _builder;

        public void Configure(EntityTypeBuilder<ProfileOperation> builder)
        {
            _builder = builder;
            _builder.ToTable("ProfileOperations");
            ConfigurePrimaryKey();
            ConfigureColumns();
            ConfigureForeignKeys();
            ConfigureIndexes();
        }

        private void ConfigureColumns()
        {
            _builder.Property(a => a.IdProfile).IsRequired(true).HasColumnName("Id_Profile");
            _builder.Property(a => a.IdOperation).IsRequired(true).HasColumnName("Id_Role");
            _builder.Property(a => a.CanCreate).HasDefaultValue(false).HasColumnName("CanCreate");
            _builder.Property(a => a.CanResearch).HasDefaultValue(false).HasColumnName("CanResearch");
            _builder.Property(a => a.CanUpdate).HasDefaultValue(false).HasColumnName("CanUpdate");
            _builder.Property(a => a.CanDelete).HasDefaultValue(false).HasColumnName("CanDelete");
            _builder.Property(a => a.CanExport).HasDefaultValue(false).HasColumnName("CanExport");
            _builder.Property(a => a.CanImport).HasDefaultValue(false).HasColumnName("CanImport");
        }

        private void ConfigureForeignKeys()
        {
            _builder.HasOne(a => a.Profile)
                .WithMany(a => a.ProfileOperations)
                .HasForeignKey(a => a.IdProfile);

            _builder.HasOne(a => a.Operation)
                .WithMany(a => a.ProfileOperations)
                .HasForeignKey(a => a.IdOperation);
        }

        private void ConfigureIndexes()
        {
            _builder.HasIndex(a =>
                new
                {
                    a.IdProfile,
                    a.IdOperation
                })
                .IsUnique(true);
        }

        private void ConfigurePrimaryKey()
        {
            _builder.HasKey(a => new
            {
                a.IdProfile,
                a.IdOperation
            });
        }
    }
}
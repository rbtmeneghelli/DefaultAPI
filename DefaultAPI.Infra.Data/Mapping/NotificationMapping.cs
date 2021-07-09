using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        private EntityTypeBuilder<Notification> _builder;

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            _builder = builder;
            _builder.ToTable("Notification");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1).HasColumnName("Id");
            _builder.Property(x => x.Description).IsRequired().HasMaxLength(100).HasColumnName("Description");
            _builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now).HasColumnName("Created_Time");
            _builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("Is_Active").HasColumnType("bit");
        }
    }
}

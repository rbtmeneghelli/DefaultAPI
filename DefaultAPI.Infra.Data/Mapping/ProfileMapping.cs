using DefaultAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.Data.Mapping
{
    public class ProfileMapping : IEntityTypeConfiguration<Profile>
    {
        private EntityTypeBuilder<Profile> _builder;

        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            _builder = builder;
            _builder.ToTable("Profiles");
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            _builder.HasKey(x => x.Id);
            _builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1,1).HasColumnName("Id");
            _builder.Property(x => x.Description).IsRequired(true).HasMaxLength(255).HasColumnName("Description");
            _builder.Property(x => x.ProfileType).IsRequired(true).HasColumnName("Profile_Type");
            _builder.Property(x => x.AccessGroup).IsRequired(true).HasColumnName("Access_Group");
            _builder.Property(x => x.LoginType).IsRequired(true).HasColumnName("Login_Type");
            _builder.Property(x => x.CreatedTime).HasColumnName("Created_Time");
            _builder.Property(x => x.UpdateTime).HasColumnName("Update_Time");
            _builder.Property(x => x.IsActive).HasDefaultValue(true).HasColumnName("Is_Active");
        }
    }
}

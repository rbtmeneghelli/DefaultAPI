﻿// <auto-generated />
using System;
using DefaultAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DefaultAPI.Infra.Data.Migrations
{
    [DbContext(typeof(DefaultAPIContext))]
    [Migration("20210724014416_new_tables_control")]
    partial class new_tables_control
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Audit", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnName("Action_Name")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 845, DateTimeKind.Local).AddTicks(372));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("KeyValues")
                        .HasColumnName("Key_Values")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("NewValues")
                        .HasColumnName("New_Values")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("OldValues")
                        .HasColumnName("Old_Values")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnName("Table_Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Ceps", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnName("District")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnName("Cep")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Complemento")
                        .HasColumnName("Complement")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(5451));

                    b.Property<string>("Ddd")
                        .HasColumnName("Ddd")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Gia")
                        .HasColumnName("Gia")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Ibge")
                        .HasColumnName("Ibge")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<long>("IdEstado")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Localidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnName("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Siafi")
                        .HasColumnName("Siafi")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnName("Uf")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdEstado");

                    b.ToTable("Ceps");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.City", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 793, DateTimeKind.Local).AddTicks(6192));

                    b.Property<long?>("IBGE")
                        .IsRequired()
                        .HasColumnName("Ibge")
                        .HasColumnType("bigint")
                        .HasMaxLength(255);

                    b.Property<long>("IdState")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasColumnName("City")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdState");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnName("Class")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MessageError")
                        .HasColumnName("Message_Error")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnName("Method")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Object")
                        .HasColumnName("Object")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Notification", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 803, DateTimeKind.Local).AddTicks(3262));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Operation", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Operation");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 848, DateTimeKind.Local).AddTicks(4715),
                            Description = "Auditoria",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Profile", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("AccessGroup")
                        .HasColumnName("Access_Group")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<byte>("LoginType")
                        .HasColumnName("Login_Type")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ProfileType")
                        .HasColumnName("Profile_Type")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccessGroup = (byte)0,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(2354),
                            Description = "Perfil Usuário Tria",
                            IsActive = true,
                            LoginType = (byte)1,
                            ProfileType = (byte)0
                        },
                        new
                        {
                            Id = 2L,
                            AccessGroup = (byte)0,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(4070),
                            Description = "Perfil Administrador Tria",
                            IsActive = true,
                            LoginType = (byte)1,
                            ProfileType = (byte)1
                        },
                        new
                        {
                            Id = 3L,
                            AccessGroup = (byte)0,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(4106),
                            Description = "Perfil Manager Tria",
                            IsActive = true,
                            LoginType = (byte)1,
                            ProfileType = (byte)2
                        });
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.ProfileOperation", b =>
                {
                    b.Property<long>("IdProfile")
                        .HasColumnName("Id_Profile")
                        .HasColumnType("bigint");

                    b.Property<long>("IdOperation")
                        .HasColumnName("Id_Role")
                        .HasColumnType("bigint");

                    b.Property<bool>("CanCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanCreate")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("CanDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanDelete")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("CanExport")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanExport")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("CanImport")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanImport")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("CanResearch")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanResearch")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("CanUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CanUpdate")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("IdProfile", "IdOperation");

                    b.HasIndex("IdOperation");

                    b.HasIndex("IdProfile", "IdOperation")
                        .IsUnique();

                    b.ToTable("ProfileOperations");

                    b.HasData(
                        new
                        {
                            IdProfile = 1L,
                            IdOperation = 1L,
                            CanCreate = false,
                            CanDelete = false,
                            CanExport = false,
                            CanImport = false,
                            CanResearch = false,
                            CanUpdate = false
                        });
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Region", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(8282));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("Initials")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Role", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Action")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<long?>("IdFuncionalidade")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<long?>("OperationId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoleTag")
                        .IsRequired()
                        .HasColumnName("Role")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Action = (byte)0,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 848, DateTimeKind.Local).AddTicks(8993),
                            Description = "Regra de acesso a tela de Auditoria",
                            IsActive = false,
                            RoleTag = "ROLE_AUDIT"
                        });
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.States", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(610));

                    b.Property<long>("IdRegiao")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("Initials")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdRegiao");

                    b.ToTable("States");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.User", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnName("Created_Time")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdProfile")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnName("Is_Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAuthenticated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Is_Authenticated")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastPassword")
                        .HasColumnName("Last_Password")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("Login")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("Password")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("Update_Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdProfile");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTime(2021, 7, 23, 22, 44, 15, 851, DateTimeKind.Local).AddTicks(6038),
                            IdProfile = 1L,
                            IsActive = true,
                            IsAuthenticated = true,
                            LastPassword = "",
                            Login = "admin@DefaultAPI.com.br",
                            Password = "AQAQJwAAwbmhC9oEcxrj8uj7F9aVU0J9Hhd2evrkwyTBJSoMyTQ="
                        });
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Ceps", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.States", "Estado")
                        .WithMany("Ceps")
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.City", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.States", "States")
                        .WithMany("City")
                        .HasForeignKey("IdState")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.ProfileOperation", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.Operation", "Operation")
                        .WithMany("ProfileOperations")
                        .HasForeignKey("IdOperation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DefaultAPI.Domain.Entities.Profile", "Profile")
                        .WithMany("ProfileOperations")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.Role", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.Operation", "Operation")
                        .WithMany("Roles")
                        .HasForeignKey("OperationId");
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.States", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.Region", "Regiao")
                        .WithMany("Estados")
                        .HasForeignKey("IdRegiao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DefaultAPI.Domain.Entities.User", b =>
                {
                    b.HasOne("DefaultAPI.Domain.Entities.Profile", "Profile")
                        .WithMany("Users")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
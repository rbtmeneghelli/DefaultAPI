using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class initial_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(8929)),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Table_Name = table.Column<string>(maxLength: 100, nullable: false),
                    Action_Name = table.Column<string>(maxLength: 80, nullable: false),
                    Key_Values = table.Column<string>(maxLength: 10000, nullable: true),
                    Old_Values = table.Column<string>(maxLength: 10000, nullable: true),
                    New_Values = table.Column<string>(maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(maxLength: 100, nullable: false),
                    Method = table.Column<string>(maxLength: 100, nullable: false),
                    Message_Error = table.Column<string>(maxLength: 10000, nullable: true),
                    Update_Time = table.Column<DateTime>(nullable: false),
                    Object = table.Column<string>(maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Profile_Type = table.Column<byte>(nullable: false),
                    Access_Group = table.Column<byte>(nullable: false),
                    Login_Type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(7582)),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Initials = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Role = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(maxLength: 120, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Last_Password = table.Column<string>(maxLength: 255, nullable: true),
                    Is_Authenticated = table.Column<bool>(nullable: false, defaultValue: false),
                    IdProfile = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Profiles_IdProfile",
                        column: x => x.IdProfile,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(3228)),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Initials = table.Column<string>(maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IdRegiao = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Regions_IdRegiao",
                        column: x => x.IdRegiao,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileRoles",
                columns: table => new
                {
                    Id_Profile = table.Column<long>(nullable: false),
                    Id_Role = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileRoles", x => new { x.Id_Profile, x.Id_Role });
                    table.ForeignKey(
                        name: "FK_ProfileRoles_Profiles_Id_Profile",
                        column: x => x.Id_Profile,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileRoles_Roles_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ceps",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(5555)),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Cep = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Complement = table.Column<string>(maxLength: 255, nullable: true),
                    District = table.Column<string>(maxLength: 255, nullable: true),
                    Localidade = table.Column<string>(nullable: true),
                    Uf = table.Column<string>(maxLength: 2, nullable: false),
                    Ibge = table.Column<string>(maxLength: 255, nullable: true),
                    Gia = table.Column<string>(maxLength: 255, nullable: true),
                    Ddd = table.Column<string>(maxLength: 255, nullable: true),
                    Siafi = table.Column<string>(maxLength: 255, nullable: true),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ceps_States_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 95, DateTimeKind.Local).AddTicks(1904)),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    City = table.Column<string>(maxLength: 255, nullable: true),
                    Ibge = table.Column<long>(maxLength: 255, nullable: false),
                    IdState = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_IdState",
                        column: x => x.IdState,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Access_Group", "Created_Time", "Description", "Is_Active", "Login_Type", "Profile_Type", "Update_Time" },
                values: new object[,]
                {
                    { 1L, (byte)0, new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(7267), "Perfil Usuário Tria", true, (byte)1, (byte)0, null },
                    { 2L, (byte)0, new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(8944), "Perfil Administrador Tria", true, (byte)1, (byte)1, null },
                    { 3L, (byte)0, new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(8968), "Perfil Manager Tria", true, (byte)1, (byte)2, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Created_Time", "Description", "Role", "Update_Time" },
                values: new object[] { 1L, new DateTime(2021, 5, 2, 16, 42, 10, 121, DateTimeKind.Local).AddTicks(2063), "Regra de acesso a tela de Auditoria", "ROLE_AUDIT", null });

            migrationBuilder.InsertData(
                table: "ProfileRoles",
                columns: new[] { "Id_Profile", "Id_Role" },
                values: new object[] { 1L, 1L });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_Time", "IdProfile", "Is_Active", "Is_Authenticated", "Last_Password", "Login", "Password", "Update_Time" },
                values: new object[] { 1L, new DateTime(2021, 5, 2, 16, 42, 10, 122, DateTimeKind.Local).AddTicks(2393), 1L, true, true, "", "admin@DefaultAPI.com.br", "AQAQJwAAaXwmQgSYz9wCpbKzgzG0Dk743NeCSrBeWx7coh2In6Y=", null });

            migrationBuilder.CreateIndex(
                name: "IX_Ceps_IdEstado",
                table: "Ceps",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdState",
                table: "Cities",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileRoles_Id_Role",
                table: "ProfileRoles",
                column: "Id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileRoles_Id_Profile_Id_Role",
                table: "ProfileRoles",
                columns: new[] { "Id_Profile", "Id_Role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_IdRegiao",
                table: "States",
                column: "IdRegiao");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdProfile",
                table: "Users",
                column: "IdProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Ceps");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ProfileRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}

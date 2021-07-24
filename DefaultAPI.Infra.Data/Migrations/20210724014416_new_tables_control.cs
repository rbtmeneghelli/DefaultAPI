using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class new_tables_control : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(3228));

            migrationBuilder.AddColumn<byte>(
                name: "Action",
                table: "Roles",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "IdFuncionalidade",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OperationId",
                table: "Roles",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(8282),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(7582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 793, DateTimeKind.Local).AddTicks(6192),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 95, DateTimeKind.Local).AddTicks(1904));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(5451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(5555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 845, DateTimeKind.Local).AddTicks(372),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(8929));

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 803, DateTimeKind.Local).AddTicks(3262)),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true),
                    Update_Time = table.Column<DateTime>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileOperations",
                columns: table => new
                {
                    Id_Profile = table.Column<long>(nullable: false),
                    Id_Role = table.Column<long>(nullable: false),
                    CanCreate = table.Column<bool>(nullable: false, defaultValue: false),
                    CanResearch = table.Column<bool>(nullable: false, defaultValue: false),
                    CanUpdate = table.Column<bool>(nullable: false, defaultValue: false),
                    CanDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    CanExport = table.Column<bool>(nullable: false, defaultValue: false),
                    CanImport = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileOperations", x => new { x.Id_Profile, x.Id_Role });
                    table.ForeignKey(
                        name: "FK_ProfileOperations_Operation_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileOperations_Profiles_Id_Profile",
                        column: x => x.Id_Profile,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Id", "Created_Time", "Description", "Is_Active", "Update_Time" },
                values: new object[] { 1L, new DateTime(2021, 7, 23, 22, 44, 15, 848, DateTimeKind.Local).AddTicks(4715), "Auditoria", true, null });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(2354), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(4070), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 44, 15, 846, DateTimeKind.Local).AddTicks(4106), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 7, 23, 22, 44, 15, 848, DateTimeKind.Local).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 44, 15, 851, DateTimeKind.Local).AddTicks(6038), true, "AQAQJwAAwbmhC9oEcxrj8uj7F9aVU0J9Hhd2evrkwyTBJSoMyTQ=" });

            migrationBuilder.InsertData(
                table: "ProfileOperations",
                columns: new[] { "Id_Profile", "Id_Role" },
                values: new object[] { 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OperationId",
                table: "Roles",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileOperations_Id_Role",
                table: "ProfileOperations",
                column: "Id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileOperations_Id_Profile_Id_Role",
                table: "ProfileOperations",
                columns: new[] { "Id_Profile", "Id_Role" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Operation_OperationId",
                table: "Roles",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Operation_OperationId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ProfileOperations");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Roles_OperationId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IdFuncionalidade",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Roles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(3228),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(7582),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 95, DateTimeKind.Local).AddTicks(1904),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 793, DateTimeKind.Local).AddTicks(6192));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(5555),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 16, 42, 10, 118, DateTimeKind.Local).AddTicks(8929),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 845, DateTimeKind.Local).AddTicks(372));

            migrationBuilder.CreateTable(
                name: "ProfileRoles",
                columns: table => new
                {
                    Id_Profile = table.Column<long>(type: "bigint", nullable: false),
                    Id_Role = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.InsertData(
                table: "ProfileRoles",
                columns: new[] { "Id_Profile", "Id_Role" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(7267), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(8944), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 16, 42, 10, 119, DateTimeKind.Local).AddTicks(8968), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 5, 2, 16, 42, 10, 121, DateTimeKind.Local).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 5, 2, 16, 42, 10, 122, DateTimeKind.Local).AddTicks(2393), true, "AQAQJwAAaXwmQgSYz9wCpbKzgzG0Dk743NeCSrBeWx7coh2In6Y=" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileRoles_Id_Role",
                table: "ProfileRoles",
                column: "Id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileRoles_Id_Profile_Id_Role",
                table: "ProfileRoles",
                columns: new[] { "Id_Profile", "Id_Role" },
                unique: true);
        }
    }
}

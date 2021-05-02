using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class new_table_city : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(4197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 662, DateTimeKind.Local).AddTicks(2444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(8484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 666, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(6515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 664, DateTimeKind.Local).AddTicks(9596));

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_Time = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 710, DateTimeKind.Local).AddTicks(6542)),
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

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 14, 14, 10, 730, DateTimeKind.Local).AddTicks(6084), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 14, 14, 10, 730, DateTimeKind.Local).AddTicks(7454), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 14, 14, 10, 730, DateTimeKind.Local).AddTicks(7517), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 5, 2, 14, 14, 10, 732, DateTimeKind.Local).AddTicks(1075));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 5, 2, 14, 14, 10, 733, DateTimeKind.Local).AddTicks(1824), true, "AQAQJwAA6a59byh/7skPQnL/KIK+IxXoOfkWX/IgLjH4EIjqijE=" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdState",
                table: "Cities",
                column: "IdState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 662, DateTimeKind.Local).AddTicks(2444),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(4197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 666, DateTimeKind.Local).AddTicks(6439),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(8484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 664, DateTimeKind.Local).AddTicks(9596),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 14, 14, 10, 729, DateTimeKind.Local).AddTicks(6515));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 2, 16, 13, 673, DateTimeKind.Local).AddTicks(5652), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 2, 16, 13, 674, DateTimeKind.Local).AddTicks(8107), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 2, 16, 13, 674, DateTimeKind.Local).AddTicks(8351), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 5, 2, 2, 16, 13, 687, DateTimeKind.Local).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 5, 2, 2, 16, 13, 695, DateTimeKind.Local).AddTicks(9930), true, "AQAQJwAAl8AiAdmtPAr8mApBkA5PYRJyl+uhr04HPHPuvL06pEw=" });
        }
    }
}

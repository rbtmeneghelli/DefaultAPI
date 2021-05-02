using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class remove_required_createdTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 662, DateTimeKind.Local).AddTicks(2444),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 190, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 666, DateTimeKind.Local).AddTicks(6439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 191, DateTimeKind.Local).AddTicks(3423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 664, DateTimeKind.Local).AddTicks(9596),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 191, DateTimeKind.Local).AddTicks(966));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 190, DateTimeKind.Local).AddTicks(4765),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 662, DateTimeKind.Local).AddTicks(2444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 191, DateTimeKind.Local).AddTicks(3423),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 666, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 2, 0, 38, 9, 191, DateTimeKind.Local).AddTicks(966),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 2, 2, 16, 13, 664, DateTimeKind.Local).AddTicks(9596));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 0, 38, 9, 192, DateTimeKind.Local).AddTicks(5724), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 0, 38, 9, 192, DateTimeKind.Local).AddTicks(8030), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 5, 2, 0, 38, 9, 192, DateTimeKind.Local).AddTicks(8108), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 5, 2, 0, 38, 9, 195, DateTimeKind.Local).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 5, 2, 0, 38, 9, 196, DateTimeKind.Local).AddTicks(6376), true, "AQAQJwAAl3Ft7BxAQ7czcfM2tYlhU/V+kddJG8mGGsUM/QSWomA=" });
        }
    }
}

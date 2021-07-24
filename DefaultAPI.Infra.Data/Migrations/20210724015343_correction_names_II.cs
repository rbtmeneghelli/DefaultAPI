using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class correction_names_II : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Operation_OperationId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_OperationId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Roles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 6, DateTimeKind.Local).AddTicks(8042),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(568));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(1908),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(4423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Notification",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 42, 980, DateTimeKind.Local).AddTicks(7902),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 41, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 42, 975, DateTimeKind.Local).AddTicks(3240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 36, DateTimeKind.Local).AddTicks(1959));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(3520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(7311));

            migrationBuilder.UpdateData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 53, 43, 9, DateTimeKind.Local).AddTicks(9647), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 53, 43, 8, DateTimeKind.Local).AddTicks(3826), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 53, 43, 8, DateTimeKind.Local).AddTicks(5139), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 53, 43, 8, DateTimeKind.Local).AddTicks(5165), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 7, 23, 22, 53, 43, 10, DateTimeKind.Local).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 53, 43, 12, DateTimeKind.Local).AddTicks(2413), true, "AQAQJwAAQxT9DYCy1PeWTyRFtjz5ClT5B9twYJ1MePTdEaFTLu4=" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IdOperation",
                table: "Roles",
                column: "IdOperation");

            migrationBuilder.AddForeignKey(
                name: "IdOperation",
                table: "Roles",
                column: "IdOperation",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "IdOperation",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_IdOperation",
                table: "Roles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(568),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 6, DateTimeKind.Local).AddTicks(8042));

            migrationBuilder.AddColumn<long>(
                name: "OperationId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(4423),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(1908));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Notification",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 41, DateTimeKind.Local).AddTicks(6851),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 42, 980, DateTimeKind.Local).AddTicks(7902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 36, DateTimeKind.Local).AddTicks(1959),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 42, 975, DateTimeKind.Local).AddTicks(3240));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(2977),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(7311),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 53, 43, 7, DateTimeKind.Local).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 50, 34, 71, DateTimeKind.Local).AddTicks(4394), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 50, 34, 69, DateTimeKind.Local).AddTicks(777), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 50, 34, 69, DateTimeKind.Local).AddTicks(2115), true });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 50, 34, 69, DateTimeKind.Local).AddTicks(2143), true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Created_Time",
                value: new DateTime(2021, 7, 23, 22, 50, 34, 71, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Authenticated", "Password" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 50, 34, 79, DateTimeKind.Local).AddTicks(9331), true, "AQAQJwAAJBQGTYzl2qeETdPKe57nuc4tEFbfSQP6u/uQHFHjPx0=" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OperationId",
                table: "Roles",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Operation_OperationId",
                table: "Roles",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

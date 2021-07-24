using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefaultAPI.Infra.Data.Migrations
{
    public partial class correction_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileOperations_Operation_Id_Role",
                table: "ProfileOperations");

            migrationBuilder.DropColumn(
                name: "IdFuncionalidade",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "Id_Role",
                table: "ProfileOperations",
                newName: "Id_Operation");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileOperations_Id_Profile_Id_Role",
                table: "ProfileOperations",
                newName: "IX_ProfileOperations_Id_Profile_Id_Operation");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileOperations_Id_Role",
                table: "ProfileOperations",
                newName: "IX_ProfileOperations_Id_Operation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(568),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(610));

            migrationBuilder.AddColumn<long>(
                name: "IdOperation",
                table: "Roles",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(4423),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Notification",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 41, DateTimeKind.Local).AddTicks(6851),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 803, DateTimeKind.Local).AddTicks(3262));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 36, DateTimeKind.Local).AddTicks(1959),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 793, DateTimeKind.Local).AddTicks(6192));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(2977),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(5451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(7311),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 845, DateTimeKind.Local).AddTicks(372));

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileOperations_Operation_Id_Operation",
                table: "ProfileOperations",
                column: "Id_Operation",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileOperations_Operation_Id_Operation",
                table: "ProfileOperations");

            migrationBuilder.DropColumn(
                name: "IdOperation",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "Id_Operation",
                table: "ProfileOperations",
                newName: "Id_Role");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileOperations_Id_Profile_Id_Operation",
                table: "ProfileOperations",
                newName: "IX_ProfileOperations_Id_Profile_Id_Role");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileOperations_Id_Operation",
                table: "ProfileOperations",
                newName: "IX_ProfileOperations_Id_Role");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "States",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(610),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(568));

            migrationBuilder.AddColumn<long>(
                name: "IdFuncionalidade",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Regions",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(8282),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(4423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Notification",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 803, DateTimeKind.Local).AddTicks(3262),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 41, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Cities",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 793, DateTimeKind.Local).AddTicks(6192),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 36, DateTimeKind.Local).AddTicks(1959));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Ceps",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 844, DateTimeKind.Local).AddTicks(5451),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Time",
                table: "Audits",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 23, 22, 44, 15, 845, DateTimeKind.Local).AddTicks(372),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 23, 22, 50, 34, 67, DateTimeKind.Local).AddTicks(7311));

            migrationBuilder.UpdateData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created_Time", "Is_Active" },
                values: new object[] { new DateTime(2021, 7, 23, 22, 44, 15, 848, DateTimeKind.Local).AddTicks(4715), true });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileOperations_Operation_Id_Role",
                table: "ProfileOperations",
                column: "Id_Role",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

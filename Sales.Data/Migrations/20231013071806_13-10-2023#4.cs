using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _131020234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsContainer",
                table: "Product",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 18, 5, 932, DateTimeKind.Local).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 18, 5, 932, DateTimeKind.Local).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 18, 5, 932, DateTimeKind.Local).AddTicks(7678));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsContainer",
                table: "Product",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 12, 33, 590, DateTimeKind.Local).AddTicks(8949));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 12, 33, 590, DateTimeKind.Local).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 12, 33, 590, DateTimeKind.Local).AddTicks(9106));
        }
    }
}

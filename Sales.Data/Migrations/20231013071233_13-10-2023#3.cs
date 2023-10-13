using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _131020233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 10, 53, 218, DateTimeKind.Local).AddTicks(3131));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 10, 53, 218, DateTimeKind.Local).AddTicks(3253));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 10, 53, 218, DateTimeKind.Local).AddTicks(3264));
        }
    }
}

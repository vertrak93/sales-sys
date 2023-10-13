using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _131020235 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

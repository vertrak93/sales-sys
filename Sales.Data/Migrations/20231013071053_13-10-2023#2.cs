using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _131020232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "Product",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 5, 24, 9, DateTimeKind.Local).AddTicks(9948));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 5, 24, 10, DateTimeKind.Local).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 13, 1, 5, 24, 10, DateTimeKind.Local).AddTicks(66));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId");
        }
    }
}

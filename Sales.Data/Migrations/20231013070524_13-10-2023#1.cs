using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _131020231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.UnitOfMeasureId);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Product");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 8, 2, 49, 31, 320, DateTimeKind.Local).AddTicks(4389));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 8, 2, 49, 31, 320, DateTimeKind.Local).AddTicks(4504));

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 8, 2, 49, 31, 320, DateTimeKind.Local).AddTicks(4517));
        }
    }
}

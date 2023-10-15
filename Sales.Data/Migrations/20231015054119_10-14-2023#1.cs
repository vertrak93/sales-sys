using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class _101420231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UnitOfMeasure",
                newName: "UnitOfMeasureName");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RefreshToken",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "RefreshToken",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasureName",
                table: "UnitOfMeasure",
                newName: "Name");
        }
    }
}

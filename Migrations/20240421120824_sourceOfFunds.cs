using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class sourceOfFunds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7356ffa0-4ad1-4796-9b33-e61b1b08509b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("919c61e3-8a1a-445b-b942-a3c217b3a826"));

            migrationBuilder.AddColumn<string>(
                name: "SourceOfFunds",
                table: "Accounts",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b960ff00-afb8-4cf9-afea-00f4520b15fe"), null, "User", "USER" },
                    { new Guid("f3c0047b-1c35-434a-9d82-e40d6e2f9016"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b960ff00-afb8-4cf9-afea-00f4520b15fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c0047b-1c35-434a-9d82-e40d6e2f9016"));

            migrationBuilder.DropColumn(
                name: "SourceOfFunds",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7356ffa0-4ad1-4796-9b33-e61b1b08509b"), null, "Admin", "ADMIN" },
                    { new Guid("919c61e3-8a1a-445b-b942-a3c217b3a826"), null, "User", "USER" }
                });
        }
    }
}

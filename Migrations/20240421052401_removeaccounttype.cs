using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class removeaccounttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1cd3b8aa-8f28-40df-aaca-f2329a562c92"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c32099a-aedf-4eff-87ed-4e862f11a93a"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0c612415-1816-4c69-aa06-b3857f5b3236"), null, "User", "USER" },
                    { new Guid("25c14846-0a30-4862-a1da-a71e249e11d0"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c612415-1816-4c69-aa06-b3857f5b3236"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("25c14846-0a30-4862-a1da-a71e249e11d0"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1cd3b8aa-8f28-40df-aaca-f2329a562c92"), null, "Admin", "ADMIN" },
                    { new Guid("9c32099a-aedf-4eff-87ed-4e862f11a93a"), null, "User", "USER" }
                });
        }
    }
}

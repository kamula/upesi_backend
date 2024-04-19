using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4a48c1e-7aa5-4ac9-a9e3-895a650a9fe5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e141efc9-657d-4fbf-ba16-adcea033dd67"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("608a8a64-f954-49aa-991f-6041b254225b"), null, "Admin", "ADMIN" },
                    { new Guid("87d10ad8-6a0c-4bbd-823b-5eb3c6aea9d5"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("608a8a64-f954-49aa-991f-6041b254225b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("87d10ad8-6a0c-4bbd-823b-5eb3c6aea9d5"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c4a48c1e-7aa5-4ac9-a9e3-895a650a9fe5"), null, "Admin", "ADMIN" },
                    { new Guid("e141efc9-657d-4fbf-ba16-adcea033dd67"), null, "User", "USER" }
                });
        }
    }
}

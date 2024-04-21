using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class removebankname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2aa16df5-62ab-45e7-be8d-daacf2343108"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7deca662-724b-4ac1-a166-ff3dcc236861"));

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "AtmWithdraws");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7356ffa0-4ad1-4796-9b33-e61b1b08509b"), null, "Admin", "ADMIN" },
                    { new Guid("919c61e3-8a1a-445b-b942-a3c217b3a826"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "BankName",
                table: "AtmWithdraws",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2aa16df5-62ab-45e7-be8d-daacf2343108"), null, "User", "USER" },
                    { new Guid("7deca662-724b-4ac1-a166-ff3dcc236861"), null, "Admin", "ADMIN" }
                });
        }
    }
}

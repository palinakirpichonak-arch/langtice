using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("419ce969-51ab-41c9-9d2f-ae0f007d3b2d"), "Russian" },
                    { new Guid("5715abc0-de4e-4a5c-bd9c-4edcbade3e09"), "French" },
                    { new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "English" },
                    { new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "German" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("419ce969-51ab-41c9-9d2f-ae0f007d3b2d"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("5715abc0-de4e-4a5c-bd9c-4edcbade3e09"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"));
        }
    }
}

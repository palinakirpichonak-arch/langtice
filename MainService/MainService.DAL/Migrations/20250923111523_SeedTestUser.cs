using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "PasswordHash", "Status", "Username" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), null, "testuser@example.com", "AQAAAAIAAYagAAAAEFakeHashedPassword1234567890", true, "testuser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"));
        }
    }
}

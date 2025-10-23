using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordHash",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("419ce969-51ab-41c9-9d2f-ae0f007d3b2d"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("5715abc0-de4e-4a5c-bd9c-4edcbade3e09"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-4789-abc1-123456789001"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-4789-abc1-123456789002"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-4789-abc1-123456789003"));

            migrationBuilder.DeleteData(
                table: "UserWords",
                keyColumns: new[] { "UserId", "WordId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789001") });

            migrationBuilder.DeleteData(
                table: "UserWords",
                keyColumns: new[] { "UserId", "WordId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789002") });

            migrationBuilder.DeleteData(
                table: "UserWords",
                keyColumns: new[] { "UserId", "WordId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789003") });

            migrationBuilder.DeleteData(
                table: "UserWords",
                keyColumns: new[] { "UserId", "WordId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789006") });

            migrationBuilder.DeleteData(
                table: "UserWords",
                keyColumns: new[] { "UserId", "WordId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789007") });

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789004"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789005"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789009"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789010"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7ff9cff2-4cf1-45db-aa70-855bb69e507d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789001"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789002"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789003"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789006"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789007"));

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abc1-123456789008"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "PasswordHash", "Status", "Username" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), null, "testuser@example.com", "AQAAAAIAAYagAAAAEFakeHashedPassword1234567890", true, "testuser" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "BaseLanguageId", "LearningLanguageId", "Status" },
                values: new object[] { new Guid("7ff9cff2-4cf1-45db-aa70-855bb69e507d"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), true });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "LanguageId", "Text" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789001"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "Hello" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789002"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "Goodbye" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789003"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "Thank you" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789004"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "Please" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789005"), new Guid("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), "Water" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789006"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "Hallo" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789007"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "Auf Wiedersehen" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789008"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "Danke" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789009"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "Bitte" },
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-123456789010"), new Guid("8e5a2463-e8d1-427a-bd84-9386e073999f"), "Wasser" }
                });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CourseId", "FromWordId", "ToWordId" },
                values: new object[,]
                {
                    { new Guid("b1b2c3d4-e5f6-4789-abc1-123456789001"), new Guid("7ff9cff2-4cf1-45db-aa70-855bb69e507d"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789001"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789006") },
                    { new Guid("b1b2c3d4-e5f6-4789-abc1-123456789002"), new Guid("7ff9cff2-4cf1-45db-aa70-855bb69e507d"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789002"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789007") },
                    { new Guid("b1b2c3d4-e5f6-4789-abc1-123456789003"), new Guid("7ff9cff2-4cf1-45db-aa70-855bb69e507d"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789003"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789008") }
                });

            migrationBuilder.InsertData(
                table: "UserWords",
                columns: new[] { "UserId", "WordId", "AddedAt" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789001"), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789002"), new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789003"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789006"), new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"), new Guid("a1b2c3d4-e5f6-4789-abc1-123456789007"), new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordHash",
                table: "Users",
                column: "PasswordHash",
                unique: true);
        }
    }
}

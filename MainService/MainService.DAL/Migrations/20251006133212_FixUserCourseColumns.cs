using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixUserCourseColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_CourseId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "Id_UserId",
                table: "UserCourses");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Courses",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id_CourseId",
                table: "UserCourses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id_UserId",
                table: "UserCourses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Courses",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);
        }
    }
}

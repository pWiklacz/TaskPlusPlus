using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaskDueDateUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationTime",
                schema: "application",
                table: "Tasks",
                newName: "DueTime");

            migrationBuilder.AddColumn<int>(
                name: "DurationTimeInMinutes",
                schema: "application",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DueTime",
                schema: "application",
                table: "Projects",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTimeInMinutes",
                schema: "application",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DueTime",
                schema: "application",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "DueTime",
                schema: "application",
                table: "Tasks",
                newName: "DurationTime");
        }
    }
}

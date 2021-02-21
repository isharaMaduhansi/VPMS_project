using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class columaddtask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSheet",
                table: "Task");

            migrationBuilder.AddColumn<bool>(
                name: "TaskComplete",
                table: "Task",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskCompletedOn",
                table: "Task",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskComplete",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TaskCompletedOn",
                table: "Task");

            migrationBuilder.AddColumn<bool>(
                name: "TimeSheet",
                table: "Task",
                type: "bit",
                nullable: true);
        }
    }
}

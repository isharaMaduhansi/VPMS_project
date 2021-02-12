using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class tempin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TempInTime",
                table: "TimeTracker",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempInTime",
                table: "TimeTracker");
        }
    }
}

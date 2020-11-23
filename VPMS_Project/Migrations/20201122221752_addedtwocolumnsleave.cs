using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class addedtwocolumnsleave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "LeaveApply",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "LeaveApply",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "LeaveApply");

            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "LeaveApply");
        }
    }
}

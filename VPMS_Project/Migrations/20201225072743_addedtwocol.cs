using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class addedtwocol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "Attendence",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Approver",
                table: "Attendence",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "Attendence");

            migrationBuilder.DropColumn(
                name: "Approver",
                table: "Attendence");
        }
    }
}

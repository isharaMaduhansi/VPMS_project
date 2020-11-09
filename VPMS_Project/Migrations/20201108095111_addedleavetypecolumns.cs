using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class addedleavetypecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Annual",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Casual",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HalfDays",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Medical",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortLeaves",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnnualAllocated",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CasualAllocated",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HalfLeaveAllocated",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicalAllocated",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortLeaveAllocated",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Todate",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annual",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Casual",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "HalfDays",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Medical",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ShortLeaves",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "AnnualAllocated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CasualAllocated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HalfLeaveAllocated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MedicalAllocated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShortLeaveAllocated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Todate",
                table: "Employees");
        }
    }
}

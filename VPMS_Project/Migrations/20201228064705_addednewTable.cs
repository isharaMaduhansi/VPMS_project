using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class addednewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarkAttendence",
                columns: table => new
                {
                    MarkAttendenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    TotalHours = table.Column<double>(nullable: false),
                    EmpId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    EmployeesEmpId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkAttendence", x => x.MarkAttendenceId);
                    table.ForeignKey(
                        name: "FK_MarkAttendence_Employees_EmployeesEmpId",
                        column: x => x.EmployeesEmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarkAttendence_EmployeesEmpId",
                table: "MarkAttendence",
                column: "EmployeesEmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkAttendence");
        }
    }
}

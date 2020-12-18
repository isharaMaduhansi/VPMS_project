using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeTracker",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    TotalHours = table.Column<double>(nullable: false),
                    BreakStart = table.Column<DateTime>(nullable: true),
                    BreakEnd = table.Column<DateTime>(nullable: true),
                    BreakingHours = table.Column<double>(nullable: false),
                    WorkingHours = table.Column<double>(nullable: false),
                    EmpId = table.Column<int>(nullable: false),
                    EmployeesEmpId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTracker", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_TimeTracker_Employees_EmployeesEmpId",
                        column: x => x.EmployeesEmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracker_EmployeesEmpId",
                table: "TimeTracker",
                column: "EmployeesEmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTracker");
        }
    }
}

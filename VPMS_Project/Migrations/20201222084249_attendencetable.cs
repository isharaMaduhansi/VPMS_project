using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class attendencetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendence",
                columns: table => new
                {
                    AttendenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    TotalHours = table.Column<double>(nullable: false),
                    BreakingHours = table.Column<double>(nullable: false),
                    WorkingHours = table.Column<double>(nullable: false),
                    Explanation = table.Column<string>(nullable: true),
                    EmpId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    EmployeesEmpId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendence", x => x.AttendenceId);
                    table.ForeignKey(
                        name: "FK_Attendence_Employees_EmployeesEmpId",
                        column: x => x.EmployeesEmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendence_EmployeesEmpId",
                table: "Attendence",
                column: "EmployeesEmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendence");
        }
    }
}

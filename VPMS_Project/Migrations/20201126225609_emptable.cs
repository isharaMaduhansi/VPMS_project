using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class emptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PMId",
                table: "Employees",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PMId",
                table: "Employees");
        }
    }
}

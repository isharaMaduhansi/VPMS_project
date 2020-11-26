using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class visiblecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Visible",
                table: "LeaveApply",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "LeaveApply");
        }
    }
}

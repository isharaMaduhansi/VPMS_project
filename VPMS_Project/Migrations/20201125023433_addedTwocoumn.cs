using Microsoft.EntityFrameworkCore.Migrations;

namespace VPMS_Project.Migrations
{
    public partial class addedTwocoumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                table: "LeaveApply",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendName",
                table: "LeaveApply",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverName",
                table: "LeaveApply");

            migrationBuilder.DropColumn(
                name: "RecommendName",
                table: "LeaveApply");
        }
    }
}

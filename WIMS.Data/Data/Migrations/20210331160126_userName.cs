using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class userName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "BugItems");

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "BugItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "BugItems");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "FeatureItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "BugItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

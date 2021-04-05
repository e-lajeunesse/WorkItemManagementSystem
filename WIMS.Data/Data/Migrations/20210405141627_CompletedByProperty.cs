using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class CompletedByProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedByName",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompletedByName",
                table: "BugItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedByName",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "CompletedByName",
                table: "BugItems");
        }
    }
}

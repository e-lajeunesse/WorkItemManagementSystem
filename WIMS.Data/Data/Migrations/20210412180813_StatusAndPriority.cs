using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class StatusAndPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "BugItems");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FeatureItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "DetailedDescription",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "FeatureItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FeatureItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DetailedDescription",
                table: "BugItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "BugItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BugItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailedDescription",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "DetailedDescription",
                table: "BugItems");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "BugItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BugItems");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FeatureItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "FeatureItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "BugItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

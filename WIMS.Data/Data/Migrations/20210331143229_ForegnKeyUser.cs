using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class ForegnKeyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BugItems");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "BugItems");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BugItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureItems_ApplicationUserId",
                table: "FeatureItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BugItems_ApplicationUserId",
                table: "BugItems",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BugItems_AspNetUsers_ApplicationUserId",
                table: "BugItems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureItems_AspNetUsers_ApplicationUserId",
                table: "FeatureItems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugItems_AspNetUsers_ApplicationUserId",
                table: "BugItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureItems_AspNetUsers_ApplicationUserId",
                table: "FeatureItems");

            migrationBuilder.DropIndex(
                name: "IX_FeatureItems_ApplicationUserId",
                table: "FeatureItems");

            migrationBuilder.DropIndex(
                name: "IX_BugItems_ApplicationUserId",
                table: "BugItems");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BugItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "FeatureItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "FeatureItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "BugItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "BugItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

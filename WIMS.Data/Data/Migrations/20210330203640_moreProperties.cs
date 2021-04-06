using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class moreProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnderId",
                table: "FeatureItems");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "FeatureItems",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "FeatureItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "BugItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "FeatureItems");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "BugItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "FeatureItems",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnderId",
                table: "FeatureItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

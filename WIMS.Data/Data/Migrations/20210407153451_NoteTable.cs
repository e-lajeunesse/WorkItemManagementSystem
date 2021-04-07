using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WIMS.MVC.Data.Migrations
{
    public partial class NoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BugItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    BugItemId = table.Column<int>(nullable: true),
                    FeatureItemId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_BugItems_BugItemId",
                        column: x => x.BugItemId,
                        principalTable: "BugItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_FeatureItems_FeatureItemId",
                        column: x => x.FeatureItemId,
                        principalTable: "FeatureItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ApplicationUserId",
                table: "Notes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_BugItemId",
                table: "Notes",
                column: "BugItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_FeatureItemId",
                table: "Notes",
                column: "FeatureItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BugItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

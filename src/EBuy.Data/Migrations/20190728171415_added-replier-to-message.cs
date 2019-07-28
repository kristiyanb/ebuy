using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EBuy.Data.Migrations
{
    public partial class addedrepliertomessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentOn",
                table: "Messages",
                newName: "SubmissionDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "RepliedOn",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplierId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReplierId",
                table: "Messages",
                column: "ReplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReplierId",
                table: "Messages",
                column: "ReplierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReplierId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReplierId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RepliedOn",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReplierId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "SubmissionDate",
                table: "Messages",
                newName: "SentOn");
        }
    }
}

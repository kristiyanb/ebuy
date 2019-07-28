using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EBuy.Data.Migrations
{
    public partial class addeddatetomessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentOn",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentOn",
                table: "Messages");
        }
    }
}

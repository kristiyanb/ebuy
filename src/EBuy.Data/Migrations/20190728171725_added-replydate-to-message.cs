using Microsoft.EntityFrameworkCore.Migrations;

namespace EBuy.Data.Migrations
{
    public partial class addedreplydatetomessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepliedOn",
                table: "Messages",
                newName: "ReplyDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplyDate",
                table: "Messages",
                newName: "RepliedOn");
        }
    }
}

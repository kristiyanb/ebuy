namespace EBuy.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class addedimagestoproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PurchasedProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PurchasedProducts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");
        }
    }
}

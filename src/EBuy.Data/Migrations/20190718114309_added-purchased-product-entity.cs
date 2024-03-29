﻿namespace EBuy.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class addedpurchasedproductentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchasedProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchasedProducts");
        }
    }
}

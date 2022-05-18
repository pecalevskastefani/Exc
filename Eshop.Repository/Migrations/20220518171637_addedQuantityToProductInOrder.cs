using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop.Repository.Migrations
{
    public partial class addedQuantityToProductInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductInOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductInOrder");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_ASP.Net.Migrations
{
    public partial class Aftermerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Product_Brand",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CategoriesFilterPanel",
                columns: table => new
                {
                    NumberOfProducts = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesFilterPanel");

            migrationBuilder.DropColumn(
                name: "Product_Brand",
                table: "Products");
        }
    }
}

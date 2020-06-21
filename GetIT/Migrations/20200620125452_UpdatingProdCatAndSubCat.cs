using Microsoft.EntityFrameworkCore.Migrations;

namespace GetIT.Migrations
{
    public partial class UpdatingProdCatAndSubCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategory",
                table: "ProductSubCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "ProductSubCategories");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "ProductCategories",
                nullable: false,
                defaultValue: 0);
        }
    }
}

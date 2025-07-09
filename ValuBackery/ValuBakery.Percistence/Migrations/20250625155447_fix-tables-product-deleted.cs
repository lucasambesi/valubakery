using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValuBakery.Percistence.Migrations
{
    /// <inheritdoc />
    public partial class fixtablesproductdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductRecipeVariant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductMaterial",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductRecipeVariant");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductMaterial");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Product");
        }
    }
}

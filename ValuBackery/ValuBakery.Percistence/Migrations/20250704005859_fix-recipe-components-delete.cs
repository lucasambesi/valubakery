using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValuBakery.Percistence.Migrations
{
    /// <inheritdoc />
    public partial class fixrecipecomponentsdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RecipeIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RecipeComponents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RecipeComponents");
        }
    }
}

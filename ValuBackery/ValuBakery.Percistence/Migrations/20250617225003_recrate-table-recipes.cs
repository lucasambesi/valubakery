using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValuBakery.Percistence.Migrations
{
    /// <inheritdoc />
    public partial class recratetablerecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVariant_RecipeId",
                table: "RecipeVariant",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeVariant_Recipes_RecipeId",
                table: "RecipeVariant",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeVariant_Recipes_RecipeId",
                table: "RecipeVariant");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_RecipeVariant_RecipeId",
                table: "RecipeVariant");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeVariant");
        }
    }
}

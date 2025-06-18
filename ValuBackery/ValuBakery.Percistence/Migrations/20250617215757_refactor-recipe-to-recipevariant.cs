using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValuBakery.Percistence.Migrations
{
    /// <inheritdoc />
    public partial class refactorrecipetorecipevariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComponents_Recipes_ChildRecipeId",
                table: "RecipeComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComponents_Recipes_ParentRecipeId",
                table: "RecipeComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeIngredients",
                newName: "RecipeVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_RecipeVariantId");

            migrationBuilder.RenameColumn(
                name: "ParentRecipeId",
                table: "RecipeComponents",
                newName: "ParentRecipeVariantId");

            migrationBuilder.RenameColumn(
                name: "ChildRecipeId",
                table: "RecipeComponents",
                newName: "ChildRecipeVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComponents_ParentRecipeId",
                table: "RecipeComponents",
                newName: "IX_RecipeComponents_ParentRecipeVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComponents_ChildRecipeId",
                table: "RecipeComponents",
                newName: "IX_RecipeComponents_ChildRecipeVariantId");

            migrationBuilder.CreateTable(
                name: "RecipeVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Portions = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeVariant", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComponents_RecipeVariant_ChildRecipeVariantId",
                table: "RecipeComponents",
                column: "ChildRecipeVariantId",
                principalTable: "RecipeVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComponents_RecipeVariant_ParentRecipeVariantId",
                table: "RecipeComponents",
                column: "ParentRecipeVariantId",
                principalTable: "RecipeVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_RecipeVariant_RecipeVariantId",
                table: "RecipeIngredients",
                column: "RecipeVariantId",
                principalTable: "RecipeVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComponents_RecipeVariant_ChildRecipeVariantId",
                table: "RecipeComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeComponents_RecipeVariant_ParentRecipeVariantId",
                table: "RecipeComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_RecipeVariant_RecipeVariantId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeVariant");

            migrationBuilder.RenameColumn(
                name: "RecipeVariantId",
                table: "RecipeIngredients",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_RecipeVariantId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_RecipeId");

            migrationBuilder.RenameColumn(
                name: "ParentRecipeVariantId",
                table: "RecipeComponents",
                newName: "ParentRecipeId");

            migrationBuilder.RenameColumn(
                name: "ChildRecipeVariantId",
                table: "RecipeComponents",
                newName: "ChildRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComponents_ParentRecipeVariantId",
                table: "RecipeComponents",
                newName: "IX_RecipeComponents_ParentRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeComponents_ChildRecipeVariantId",
                table: "RecipeComponents",
                newName: "IX_RecipeComponents_ChildRecipeId");

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComponents_Recipes_ChildRecipeId",
                table: "RecipeComponents",
                column: "ChildRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeComponents_Recipes_ParentRecipeId",
                table: "RecipeComponents",
                column: "ParentRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

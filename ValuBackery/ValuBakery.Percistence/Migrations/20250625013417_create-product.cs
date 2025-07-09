using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValuBakery.Percistence.Migrations
{
    /// <inheritdoc />
    public partial class createproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplyProfitToRecipes = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ApplyProfitToMaterials = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RecipeVariantId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMaterial_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecipeVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RecipeVariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecipeVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRecipeVariant_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecipeVariant_RecipeVariant_RecipeVariantId",
                        column: x => x.RecipeVariantId,
                        principalTable: "RecipeVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterial_MaterialId",
                table: "ProductMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterial_ProductId",
                table: "ProductMaterial",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecipeVariant_ProductId",
                table: "ProductRecipeVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecipeVariant_RecipeVariantId",
                table: "ProductRecipeVariant",
                column: "RecipeVariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMaterial");

            migrationBuilder.DropTable(
                name: "ProductRecipeVariant");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

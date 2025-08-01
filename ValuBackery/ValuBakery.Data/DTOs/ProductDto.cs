using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal ApplyProfitToRecipes { get; set; }

        public decimal ApplyProfitToMaterials { get; set; }

        public decimal Total { get; set; }

        public bool IsDeleted { get; set; }

        public List<ProductRecipeVariantDto> ProductRecipeVariants { get; set; }

        public List<ProductMaterialDto> ProductMaterials { get; set; }

        public void SetTotal()
        {
            decimal total = 0;

            total += ProductRecipeVariants.Sum(x => x.GetTotal(ApplyProfitToRecipes));
            total += ProductMaterials.Sum(x => x.GetTotal(ApplyProfitToMaterials));

            Total = total;
        }

        public string GetComponents()
        {
            var result = string.Empty;
            var parts = new List<string>();

            // Recetas
            if (ProductRecipeVariants?.Count > 0)
            {
                var recetas = ProductRecipeVariants
                    .Select(c => $"{c.RecipeVariant?.GetName()} x{c.Quantity.ToString("N0")} {UnitEnum.Ud}")
                    .ToList();

                parts.Add($"<strong>Recetas:</strong> {string.Join(", ", recetas)}. ");
            }

            // Materiales
            if (ProductMaterials?.Count > 0)
            {
                var materials = ProductMaterials
                    .Select(c => $"{c.Material?.Name} x{c.Quantity.ToString("N0")} {UnitEnum.Ud}")
                    .ToList();

                parts.Add($"<strong>Materiales:</strong> {string.Join(", ", materials)}. ");
            }

            return result;
        }
    }
}

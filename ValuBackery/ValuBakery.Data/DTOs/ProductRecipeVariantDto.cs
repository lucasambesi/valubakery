using System.ComponentModel;
using ValuBakery.Data.Entities;

namespace ValuBakery.Data.DTOs
{
    public class ProductRecipeVariantDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int RecipeVariantId { get; set; }

        public RecipeVariantDto RecipeVariant { get; set; }

        public decimal Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public decimal GetTotal(decimal profit)
        {
            var cost = RecipeVariant.GetCost();
            return (cost + (cost * profit / 100)) * Quantity;
        }
    }
}

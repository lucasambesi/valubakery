using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class ProductRecipeVariant : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int RecipeVariantId { get; set; }

        public RecipeVariant RecipeVariant { get; set; }

        public decimal Quantity { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

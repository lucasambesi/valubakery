using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class RecipeIngredient : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int RecipeVariantId { get; set; }
        public RecipeVariant RecipeVariant { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public decimal Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}

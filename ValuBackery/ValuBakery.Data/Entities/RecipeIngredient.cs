using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class RecipeIngredient : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public decimal Quantity { get; set; }
    }

}

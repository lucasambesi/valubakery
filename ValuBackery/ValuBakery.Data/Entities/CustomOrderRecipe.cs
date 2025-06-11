using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class CustomOrderRecipe : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int CustomOrderId { get; set; }
        public CustomOrder CustomOrder { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public decimal RecipeAmount { get; set; }
    }

}

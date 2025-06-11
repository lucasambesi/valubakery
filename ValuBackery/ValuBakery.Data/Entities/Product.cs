using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Product : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public List<ProductMaterial> Materials { get; set; }
    }

}

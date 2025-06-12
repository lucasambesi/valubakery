using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Product : AuditableBaseEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public decimal Profit { get; set; }

        public decimal ApplyProfitToMaterials { get; set; }

        public int RecipeId { get; set; }

        public decimal RecipeAmount { get; set; }
        
        public Recipe Recipe { get; set; }
        
        public List<ProductMaterial> ProductMaterials { get; set; }
    }
}

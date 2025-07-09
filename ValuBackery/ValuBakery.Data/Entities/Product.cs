using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Product : AuditableBaseEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal ApplyProfitToRecipes { get; set; }

        public decimal ApplyProfitToMaterials { get; set; }

        public int RecipeVariantId { get; set; }
        
        public List<ProductRecipeVariant> ProductRecipeVariants { get; set; }
        
        public List<ProductMaterial> ProductMaterials { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

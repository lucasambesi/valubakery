using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Recipe : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public List<RecipeVariant> Variants { get; set; } = new();
    }
}

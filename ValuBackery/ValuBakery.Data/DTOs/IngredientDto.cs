using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class IngredientDto : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UnitEnum Unit { get; set; }

        public decimal CostPerUnit { get; set; }

        public bool IsDeleted { get; set; }
    }
}

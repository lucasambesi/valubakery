using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class MaterialDto : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal CostPerUnit { get; set; }

        public UnitMaterialEnum Unit { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

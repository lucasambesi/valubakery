using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Material : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitCost { get; set; }
    }
}

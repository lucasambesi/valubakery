using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Entities;

namespace ValuBakery.Data.DTOs
{
    public class ProductMaterialDto : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int MaterialId { get; set; }

        public MaterialDto Material { get; set; }

        public decimal Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public decimal GetTotal(decimal profit)
        {
            var cost = Material.CostPerUnit;
            return (cost + (cost * profit / 100)) * Quantity;
        }
    }
}

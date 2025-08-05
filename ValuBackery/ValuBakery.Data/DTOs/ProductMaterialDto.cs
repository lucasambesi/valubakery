using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

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

        public decimal GetCost()
        {
            switch (Material.Unit)
            {
                case UnitMaterialEnum.Kg:
                case UnitMaterialEnum.Lt:
                    return Quantity * Material.CostPerUnit / 1000;
                case UnitMaterialEnum.Meter:
                case UnitMaterialEnum.Roll:
                    return Quantity * Material.CostPerUnit / 100;
                case UnitMaterialEnum.Grs:
                case UnitMaterialEnum.Ud:
                case UnitMaterialEnum.Box:
                case UnitMaterialEnum.Mls:
                    return Quantity * Material.CostPerUnit;
                default:
                    return 0;
            }
        }

        public UnitMaterialEnum GetConvertUnit()
        {
            switch (Material.Unit)
            {
                case UnitMaterialEnum.Kg:
                case UnitMaterialEnum.Grs:
                    return UnitMaterialEnum.Grs;
                case UnitMaterialEnum.Lt:
                case UnitMaterialEnum.Mls:
                    return UnitMaterialEnum.Mls;
                case UnitMaterialEnum.Ud:
                    return UnitMaterialEnum.Ud;
                case UnitMaterialEnum.Box:
                    return UnitMaterialEnum.Box;
                case UnitMaterialEnum.Meter:
                case UnitMaterialEnum.Roll:
                case UnitMaterialEnum.Cm:
                    return UnitMaterialEnum.Cm;
                default:
                    return UnitMaterialEnum.None;
            }
        }
    }
}

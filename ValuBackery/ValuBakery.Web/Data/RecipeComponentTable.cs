using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Data
{
    public class RecipeComponentTable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UnitEnum Unit { get; set; }

        public decimal CostPerUnit { get; set; }

        public decimal Quantity { get; set; }

        public RecipeComponentType Type { get; set; }

        public decimal GetCost()
        {
            switch (Unit)
            {
                case UnitEnum.Kg:
                case UnitEnum.Lt:
                    return Quantity * CostPerUnit / 1000;
                case UnitEnum.Grs:
                case UnitEnum.Ud:
                case UnitEnum.Mls:
                    return Quantity * CostPerUnit;
                default:
                    return 0;
            }
        }

        public UnitEnum GetConvertUnit()
        {
            switch (Unit)
            {
                case UnitEnum.Kg:
                case UnitEnum.Grs:
                    return UnitEnum.Grs;
                case UnitEnum.Lt:
                case UnitEnum.Mls:
                    return UnitEnum.Mls;
                case UnitEnum.Ud:
                    return UnitEnum.Ud;
                default:
                    return UnitEnum.None;
            }
        }
    }
}

using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class RecipeIngredientDto
    {
        public int Id { get; set; }

        public int RecipeVariantId { get; set; }
        public RecipeVariantDto RecipeVariant { get; set; }

        public int IngredientId { get; set; }

        public IngredientDto Ingredient { get; set; }

        public decimal Quantity { get; set; }

        public decimal GetCost()
        {
            switch (Ingredient.Unit)
            {
                case UnitEnum.Kg:
                case UnitEnum.Lt:
                    return Quantity * Ingredient.CostPerUnit / 1000;
                case UnitEnum.Grs:
                case UnitEnum.Ud:
                case UnitEnum.Mls:
                    return Quantity * Ingredient.CostPerUnit;
                default:
                    return 0;
            }
        }

        public UnitEnum GetConvertUnit()
        {
            switch (Ingredient.Unit)
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
